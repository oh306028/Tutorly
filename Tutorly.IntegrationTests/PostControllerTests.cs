using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;
using System.Security.Claims;
using Tutorly.Application.Dtos;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using Tutorly.Infrastructure;
using Tutorly.WebAPI.Services;

namespace Tutorly.IntegrationTests
{
    
    public class PostControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public PostControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _mockUserContextService = new Mock<IUserContextService>();

            _client = _factory.WithWebHostBuilder(host =>
            {
                host.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<TutorlyDbContext>));
                    if (descriptor != null) services.Remove(descriptor);

                        services.AddDbContext<TutorlyDbContext>(options =>
                        options.UseInMemoryDatabase("InMemoryDB")); 
                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    

                });
            }).CreateClient();
           
        }

        private void SeedTestData(TutorlyDbContext context)
        {
            context.Users.Add(new Tutor { FirstName = "Tutor", LastName = "Test", Email = "test@gmail", PasswordHash = "sdgdsgo;pjdgagaaw", Role = Role.Tutor });
            context.Categories.Add(new Category
            {
                Id = 1,
                Name = "Test Category"
            });

            context.SaveChanges();
        }


        [Fact]
        public async Task CreatePost_ReturnsCreated()   
        {

            int tutorId = 0;
            var client = _factory.WithWebHostBuilder(host =>
            {
                host.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<TutorlyDbContext>));
                    if (descriptor != null) services.Remove(descriptor);

                    services.AddDbContext<TutorlyDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDB"));
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();


                    using var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<TutorlyDbContext>();
                    SeedTestData(context);
                    tutorId = context.Users.FirstOrDefault(t => t.LastName == "Test").Id;

                });
            }).CreateClient();


            var createPostDto = new CreatePostDto()
            {

                TutorId = tutorId,
                CategoryId = 1,
                MaxStudentAmount = 3,
                HappensOn = DayOfWeek.Monday,
                HappensAt = TimeSpan.Parse("17:00:00"),
                IsRemotely = false,
                StudentsGrade = Grade.Secondary

            };

            

            var response = await client 
                        .PostAsJsonAsync("api/posts",createPostDto);


            response.StatusCode.Should().Be(HttpStatusCode.Created);

        }





        [Fact]
        public async Task GetPosts_ValidRequest_Returns200()
        {
            var response = await _client.GetAsync("api/posts");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreatePost_InValidRequest_Returns400()        
        {   
            var request = new CreatePostDto()
            {
                
            };

            var response = await _client.PostAsJsonAsync("api/posts",request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        }


        [Fact]
        public async Task PostApply_ValidStudentRequest_ReturnsOk()
        {

            var mockPostRepository = new Mock<IRepository<Post>>();
            var mockStudentRepository = new Mock<IRepository<Student>>();
            var mockUserContextService = new Mock<IUserContextService>();

            var postId = 1;
            var studentId = 1;

            var student = new Student { Id = studentId, FirstName = "Test", LastName = "Test" };
            var post = new Post { Id = postId, MaxStudentAmount = 1 };


            mockPostRepository.Setup(r => r.GetByIdAsync(postId)).ReturnsAsync(post);
            mockStudentRepository.Setup(r => r.GetByIdAsync(studentId)).ReturnsAsync(student);
            mockUserContextService.Setup(s => s.GetUserId).Returns(studentId);

            var client = _factory.WithWebHostBuilder(host =>
            {
                host.ConfigureServices(services =>
                {
                    services.AddSingleton(mockPostRepository.Object);
                    services.AddSingleton(mockStudentRepository.Object);
                    services.AddSingleton(mockUserContextService.Object);
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            }).CreateClient();


            var response = await client.PostAsync($"api/posts/{postId}", null);


            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }


        //[Fact]
        //Cannot test static method and mocking the AuthorizationService is not done yet
        public async Task DeletePost_ValidRequest_RemovesPostFromDatabase()
        {
            // Arrange


            var category = new Category()
            {
                Name = "Maths"
            };

            var tutor = new Tutor()
            {
                Email = "test@mail",
                FirstName = "Tom",
                LastName = "Test",
                PasswordHash = "dsnldgnjlagasg",
                Role = Role.Tutor
            };

            Post currentPost;
            int tutorId = 0;
            int postId = 0;

            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TutorlyDbContext>();
                dbContext.Categories.Add(category);
                dbContext.Users.Add(tutor);
                await dbContext.SaveChangesAsync();

                tutorId = dbContext.Users.FirstOrDefault(n => n.Email == "test@mail").Id;

                var post = new Post
                {
                    MaxStudentAmount = 2,
                    TutorId = tutor.Id,
                    CategoryId = category.Id,
                    HappensAt = TimeSpan.Parse("17:00:00"),
                    HappensOn = DayOfWeek.Monday
                };

                dbContext.Posts.Add(post);

                await dbContext.SaveChangesAsync();

                currentPost = dbContext.Posts.FirstOrDefault(p => p.TutorId == tutorId);
                postId = currentPost.Id;
            }

            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, tutorId.ToString())
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);

            var userContextServiceMock = new Mock<IUserContextService>();
            userContextServiceMock.Setup(c => c.User).Returns(user);


            var authorizationMock = Mock.Of<IAuthorizationService>();

           
            Mock.Get(authorizationMock)
                .Setup(service => service.AuthorizeAsync(
                    It.IsAny<ClaimsPrincipal>(),
                    It.IsAny<Post>(),
                    It.IsAny<IAuthorizationRequirement>()
                ))
                .ReturnsAsync(AuthorizationResult.Success);


            var client = _factory.WithWebHostBuilder(host =>
            {
                host.ConfigureServices(services =>
                {
                    services.AddSingleton(userContextServiceMock.Object);
              
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<TutorlyDbContext>));
                    if (descriptor != null) services.Remove(descriptor);

                    services.AddDbContext<TutorlyDbContext>(options =>
                        options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            }).CreateClient();

            // Act
            var response = await client.DeleteAsync($"api/posts/{postId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

          
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TutorlyDbContext>();
                var deletedPost = await dbContext.Posts.FindAsync(postId);
                deletedPost.Should().BeNull(); 
            }
        }



    }
}
