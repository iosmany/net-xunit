using Microsoft.AspNetCore.Mvc.Testing;
using NetXUnit.Webapi;
using FluentAssertions;
using Newtonsoft.Json;
using static NetXUnit.Webapi.Program;

namespace NetXUnit.Specs.Integration
{
    public class CreateWebapiEndpointsTest : IClassFixture<WebApplicationFactory<Program>>
    {
        readonly private WebApplicationFactory<Program> _factory;

        public CreateWebapiEndpointsTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Testing create a webapi client
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateWebapiClient()
        {
            // Arrange
            var client = _factory.CreateClient();
            Assert.NotNull(client);
            Assert.NotNull(client.BaseAddress);
            Assert.Contains("localhost", client.BaseAddress.ToString());
            // Act
            var response = await client.GetAsync("/weatherforecast");
            // Assert
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Testing getting weather forecast endpoint
        /// [using fluentassertions lib]
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetWeatherForecastEnpoint()
        {
            // Arrange
            var client = _factory.CreateClient();
            // Act
            var response = await client.GetAsync("/weatherforecast");
            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var weatherForecasts = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);
            weatherForecasts.Should().NotBeNullOrEmpty();
            weatherForecasts.Should().HaveCount(5);
        }
    }
}
