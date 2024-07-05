using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Book_subscription.Server.API.DTOs.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Book_subscription.tests.Controllers.Integration
{
    public class AuthControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public AuthControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_ValidRegistration_ReturnsSuccess()
        {
            // Arrange
            var registerDto = new RegisterUserDTO
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "Test@123"
            };

            var content = new StringContent(JsonConvert.SerializeObject(registerDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/auth/register", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<UserResponseDTO>(responseContent);

            Assert.NotNull(responseObject);
            Assert.Equal(registerDto.UserName, responseObject.UserName);
            Assert.Equal(registerDto.Email, responseObject.Email);
            Assert.NotNull(responseObject.Token);

            // Verify content type of request
            Assert.Equal("application/json", content.Headers.ContentType.MediaType);
        }

        [Fact]
        public async Task Login_ValidLogin_ReturnsSuccess()
        {
            // Arrange
            var loginDto = new LoginUserDTO
            {
                Email = "testuser@example.com",
                Password = "Test@123"
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/auth/login", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<UserResponseDTO>(responseContent);

            Assert.NotNull(responseObject);
            Assert.Equal(loginDto.Email, responseObject.Email);
            Assert.NotNull(responseObject.Token);
        }
    }
}
