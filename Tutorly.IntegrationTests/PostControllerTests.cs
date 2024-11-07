using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Tutorly.IntegrationTests
{
    
    public class PostControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PostControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task GetPosts_ValidRequest_Returns200()
        {
            var response = await _client.GetAsync("api/posts");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }



    }
}
