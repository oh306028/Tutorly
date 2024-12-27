using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tutorly.Application.Dtos;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using Tutorly.Infrastructure;
using Tutorly.Infrastructure.Repos;
using Tutorly.WebAPI.Services;

namespace Tutorly.IntegrationTests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;
        private Mock<IUserContextService> _userContextMock;

        public UserControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(host =>
            {
                host.ConfigureServices(services =>
                {
                     _userContextMock = new Mock<IUserContextService>();

                    var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<TutorlyDbContext>));
                    services.Remove(descriptor);

                    services.AddDbContext<TutorlyDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbUsers");
                        
                    });

                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.AddSingleton(_userContextMock.Object);
                    using var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<TutorlyDbContext>();
                    SeedUsersSampleData(context);


                });
            }).CreateClient();


        }   

        private void SeedUsersSampleData(TutorlyDbContext context)
        {
            var user = new Student()
            {
                FirstName = "Test",
                LastName = "Testowo",
                Email = "Test@gmail",
                PasswordHash = "opgdjssgdjdsg",
                Role = Role.Student
            };

            context.Users.Add(user);
            context.SaveChanges();

        }

        [Fact]
        public async Task RegisterUser_ValidData_ReturnsCreated()
        {

            //arrange
            var registerData = new RegisterUserDto()
            {
                Email = "hamerl@gma.com",
                Password = "sgsdgawass",
                ConfirmPassword = "sgsdgawass",
                FirstName = "Oskar",
                LastName = "Hamer",
                Role = "Tutor"
            };

            //act
            var response = await _client.PostAsJsonAsync("api/accounts/register", registerData);


            //assert

            response.StatusCode.Should().Be(HttpStatusCode.Created);


        }


        [Fact]
        public async Task UpdateUser_ValidData_ReturnsOk()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var user = new ClaimsPrincipal(identity);


            _userContextMock.Setup(c => c.User).Returns(user);

            _userContextMock
                .Setup(c => c.GetUserId).Returns(1);

            var request = new UpdateUserDto()
            {
                FirstName = "Andrej",
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PatchAsync("api/accounts/1", jsonContent);

            response.StatusCode.Should().Be(HttpStatusCode.OK);




            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            var userResult = await _client.GetAsync("api/accounts/1");  
            var stringResult = await userResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Student>(stringResult, options); 

            result.FirstName.Should().Be("Andrej");
            
        }





        [Theory]
        [InlineData("password", "password1")]
        [InlineData("password", "Password")]
        [InlineData("password", "passworD")]
        [InlineData("password", "")]
        [InlineData("password", null)]
        public async Task RegisterUser_InvalidPasswordData_ReturnsBadRequest(string password, string confirmPassword)  
        {
            //arrange   
            var registerData = new RegisterUserDto()
            {
                Email = "hamerl@gma.com",
                Password = password,
                ConfirmPassword = confirmPassword,
                FirstName = "Oskar",
                LastName = "Hamer",
                Role = "Tutor"
            };

            //act
            var response = await _client.PostAsJsonAsync("api/accounts/register", registerData);


            //assert

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
