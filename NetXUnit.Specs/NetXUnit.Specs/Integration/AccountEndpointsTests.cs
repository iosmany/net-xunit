using Microsoft.AspNetCore.Mvc.Testing;
using NetXUnit.Webapi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NetXUnit.Specs.Integration
{
    public class AccountEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly private WebApplicationFactory<Program> _factory;

        public AccountEndpointsTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetUsers_ReturnsUsers()
        {
            // Arrange
            var client = _factory.CreateClient();
            const string endpoint = "/api/account/login";
            // Act
            var response = await client.PostAsJsonAsync(endpoint, new { email = "", password = "" });

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
