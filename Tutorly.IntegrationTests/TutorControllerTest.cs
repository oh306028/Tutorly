using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Tutorly.Application.Dtos.Params;

namespace Tutorly.IntegrationTests
{
    public class TutorControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TutorControllerTest(WebApplicationFactory<Program> factory)
        {
             _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllTutors_ReturnsOK()
        {
            var response = await _client.GetAsync("api/tutors");
                
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
