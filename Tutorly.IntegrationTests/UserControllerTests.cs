using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Tutorly.Application.Dtos;
using Tutorly.Domain.Models;

namespace Tutorly.IntegrationTests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;

        public UserControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
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
    }
}
