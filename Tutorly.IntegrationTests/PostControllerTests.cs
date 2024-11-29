using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;
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
                    d => d.ServiceType ==
                        typeof(DbContextOptions<TutorlyDbContext>));
                    services.Remove(descriptor);

                    services.AddDbContext<TutorlyDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDB");
                    });
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            }).CreateClient();
           
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


        [Fact]
        public async Task DeletePost_ValidRequest_RemovesPostFromList()
        {
        
            var student = new Student();
            var post = new Post() { Id = 1, MaxStudentAmount = 2 };
            post.AddStudent(student);

            var posts = new List<Post> { post };

            var postRepoMock = new Mock<IRepository<Post>>();
            postRepoMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => posts.FirstOrDefault(p => p.Id == id));

            postRepoMock
                .Setup(repo => repo.DeleteAsync(It.IsAny<Post>()))
                .Callback<Post>(p => posts.Remove(p));

            var client = _factory.WithWebHostBuilder(host =>
            {
                host.ConfigureServices(services =>
                {
                    services.AddSingleton(postRepoMock.Object);
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                });
            }).CreateClient();

           
            var response = await client.DeleteAsync("api/posts/1");


            response.StatusCode.Should().Be(HttpStatusCode.OK);
            posts.Should().BeEmpty(); 
        }



    }
}
