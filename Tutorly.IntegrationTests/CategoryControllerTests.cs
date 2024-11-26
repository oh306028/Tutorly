using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Tutorly.IntegrationTests
{
    public class CategoryControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        public CategoryControllerTests(WebApplicationFactory<Program> factory)
        {
            client = factory.CreateClient();
        }


        [Fact]
        public async Task GetCategories_ReturnsOk()
        {
            var response = await client.GetAsync("api/categories");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
