using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Tutorly.Application.Dtos;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using Tutorly.Infrastructure;
using Tutorly.Infrastructure.Repos;

namespace Tutorly.IntegrationTests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;
        private readonly TutorlyDbContext _context;
        private readonly WebApplicationFactory<Program> _factory;

        public UserControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(host =>
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


                });
            }).CreateClient();

            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<TutorlyDbContext>();

            _factory = factory;
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
            var client = _factory.WithWebHostBuilder(host =>
            {
                host.ConfigureServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<TutorlyDbContext>));
                    services.Remove(descriptor);

                    services.AddDbContext<TutorlyDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDB");
                    });
                });
            }).CreateClient();


            var student = _context.Users.FirstOrDefault(n => n.FirstName == "Tom");

            var request = new UpdateUserDto()
            {
                FirstName = "Andrej",
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var response = await client.PatchAsync($"api/accounts/{student.Id}", jsonContent);

       
            response.StatusCode.Should().Be(HttpStatusCode.OK);

          
            var updatedStudent = _context.Users.FirstOrDefault(n => n.LastName == "Hamerla");
            updatedStudent.FirstName.Should().Be("Andrej");
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
