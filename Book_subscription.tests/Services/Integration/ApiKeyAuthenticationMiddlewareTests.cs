/*using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Book_subscription.tests.Middlewares.Integration
{
    public class ApiKeyAuthenticationMiddlewareTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ApiKeyAuthenticationMiddlewareTests()
        {
            // Setup the test server
            _server = new TestServer(new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    // Mock IResellerService
                    var mockResellerService = new Mock<IResellerService>();
                    mockResellerService.Setup(svc => svc.GetResellerByApiKeyAsync(It.IsAny<string>()))
                                       .ReturnsAsync(new Reseller()); // Provide a mock Reseller for valid API key

                    services.AddSingleton(mockResellerService.Object); // Register the mock service

                    services.AddLogging();
                    services.AddTransient<ApiKeyAuthenticationMiddleware>();
                })
                .Configure(app =>
                {
                    app.UseMiddleware<ApiKeyAuthenticationMiddleware>();
                    app.Run(async context =>
                    {
                        await context.Response.WriteAsync("Hello from Test Server");
                    });
                }));

            // Create HttpClient for sending requests
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task Middleware_WithoutApiKey_ShouldReturnUnauthorized()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("API key is missing", responseString.Trim());
        }

        [Fact]
        public async Task Middleware_WithInvalidApiKey_ShouldReturnUnauthorized()
        {
            // Arrange
            var apiKey = "invalid_api_key";

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, "/");
            request.Headers.Add("ApiKey", apiKey);
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Invalid API Key", responseString.Trim());
        }

        [Fact]
        public async Task Middleware_WithValidApiKey_ShouldProceedToNextMiddleware()
        {
            // Arrange
            var validApiKey = "valid_api_key";

            // Act
            var request = new HttpRequestMessage(HttpMethod.Get, "/");
            request.Headers.Add("ApiKey", validApiKey);
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Hello from Test Server", responseString.Trim());
        }
    }
}
*/