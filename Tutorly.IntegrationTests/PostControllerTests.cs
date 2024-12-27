using FluentAssertions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;
using System.Security.Claims;
using Tutorly.Application.Commands;
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
        private readonly Mock<IUserContextService> _userContextServiceMock;
        private readonly Mock<IRepository<Student>> _studentRepoMock;

        public PostControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _mockUserContextService = new Mock<IUserContextService>();


            _userContextServiceMock = new Mock<IUserContextService>();
            
            _studentRepoMock = new Mock<IRepository<Student>>();
            


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

                        services.AddSingleton(_userContextServiceMock.Object);
                        services.AddSingleton(_studentRepoMock.Object);


                        using var serviceProvider = services.BuildServiceProvider();
                        using var scope = serviceProvider.CreateScope();
                        var context = scope.ServiceProvider.GetRequiredService<TutorlyDbContext>();
                        SeedTestData(context);

                });
            }).CreateClient();
           
        }

        private void SeedTestData(TutorlyDbContext context)
        {
            context.Categories.Add(new Category
            {
                Name = "Test Category"
            });

            context.SaveChanges();

            var testTutor = new Tutor()
            {
                FirstName = "Tutor",
                LastName = "Test",
                Email = "test@gmail",
                PasswordHash = "sdgdsgo;pjdgagaaw",
                Role = Role.Tutor
            };


            context.Users.Add(testTutor);
            context.SaveChanges();

            var category = context.Categories.First();

            var testPost = new Post()
            {
                Tutor = testTutor,
                CategoryId = 1,
                HappensAt = TimeSpan.Parse("14:00:00"),
                HappensOn = DayOfWeek.Monday,
                MaxStudentAmount = 3

            };

            context.Posts.Add(testPost);
            context.SaveChanges();

        }



        [Fact]
        public async Task CreatePost_ReturnsCreated()   
        {
        
            var createPostDto = new CreatePostDto()
            {

                TutorId = 1,
                CategoryId = 1,
                MaxStudentAmount = 3,
                HappensOn = DayOfWeek.Monday,
                HappensAt = TimeSpan.Parse("17:00:00"),
                IsRemotely = false,
                StudentsGrade = Grade.Secondary

            };

            
            var response = await _client 
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
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);


            _userContextServiceMock.Setup(c => c.User).Returns(user);

            _userContextServiceMock
                .Setup(c => c.GetUserId).Returns(1);

            _studentRepoMock
                .Setup(c => c.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Student() { Id = 1 }));


            var response = await _client.PostAsync($"api/posts/1", null);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

      
        }

     

        [Fact]
        public async Task DeletePost_ValidRequest_RemovesPostFromDatabase()
        {
            var createPostDto = new CreatePostDto()
            {

                TutorId = 1,
                CategoryId = 1,
                MaxStudentAmount = 3,
                HappensOn = DayOfWeek.Monday,
                HappensAt = TimeSpan.Parse("22:00:00"),
                IsRemotely = false,
                StudentsGrade = Grade.Secondary

            };

            await _client
                  .PostAsJsonAsync("api/posts", createPostDto);




            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);


            _userContextServiceMock.Setup(c => c.User).Returns(user);

            _userContextServiceMock
                .Setup(c => c.GetUserId).Returns(1);

            int postIdToDelete = 2;



            var response = await _client.DeleteAsync($"api/posts/{postIdToDelete}");


            response.StatusCode.Should().Be(HttpStatusCode.OK);
          
        }


        [Fact]
        public async Task DeletePost_InvalidAuthorization_Returns403()  
        {

            var claims = new List<Claim>
            {
                //The authorized tutor has ID == 1
                new Claim(ClaimTypes.NameIdentifier, "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);


            _userContextServiceMock.Setup(c => c.User).Returns(user);

            _userContextServiceMock
                .Setup(c => c.GetUserId).Returns(1);

            int postIdToDelete = 1;

            var response = await _client.DeleteAsync($"api/posts/{postIdToDelete}");


            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

        }


    }
}
