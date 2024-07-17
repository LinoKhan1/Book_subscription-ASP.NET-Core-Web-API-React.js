/*using System;
using System.Threading.Tasks;
using Book_subscription.Server.Core.Configurations;
using Book_subscription.Server.Core.Services;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using Xunit;

namespace Book_subscription.tests.Services.Integration
{
    public class JwtAuthServiceIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public JwtAuthServiceIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GenerateJwtTokenAsync_Successful()
        {
            // Arrange
            var jwtSettings = new JwtSettings
            {
                Key = "4756abREang9m7J+PTV19Qasdf9PFnabCdJFgJadgj0=",
                Issuer = "localhost",
                Audience = "localhost",
                ExpireHours = 24
            };

            var jwtAuthService = new JwtAuthService(Options.Create(jwtSettings));

            // Act
            var userId = "testUserId";
            var userName = "testUser";
            var token = await jwtAuthService.GenerateJwtTokenAsync(userId, userName);

            // Assert
            Assert.False(string.IsNullOrEmpty(token));

            // Example: Validate token using a JWT library
            var tokenIsValid = ValidateToken(token, jwtSettings);
            Assert.True(tokenIsValid);
        }

        [Fact]
        public async Task VerifyJwtTokenAsync_ValidToken_ReturnsTrue()
        {
            // Arrange
            var jwtSettings = new JwtSettings
            {
                Key = "4756abREang9m7J+PTV19Qasdf9PFnabCdJFgJadgj0=",
                Issuer = "localhost",
                Audience = "localhost",
                ExpireHours = 24
            };

            var jwtAuthService = new JwtAuthService(Options.Create(jwtSettings));
            var token = await jwtAuthService.GenerateJwtTokenAsync("testUserId", "testUser");

            // Act
            var tokenIsValid = await jwtAuthService.VerifyJwtTokenAsync(token);

            // Assert
            Assert.True(tokenIsValid);
        }

        [Fact]
        public async Task VerifyJwtTokenAsync_InvalidToken_ReturnsFalse()
        {
            // Arrange
            var jwtSettings = new JwtSettings
            {
                Key = "4756abREang9m7J+PTV19Qasdf9PFnabCdJFgJadgj0=",
                Issuer = "localhost",
                Audience = "localhost",
                ExpireHours = 24
            };

            var jwtAuthService = new JwtAuthService(Options.Create(jwtSettings));

            // Act
            var tokenIsValid = await jwtAuthService.VerifyJwtTokenAsync("invalid-token");

            // Assert
            Assert.False(tokenIsValid);
        }

        // Helper method to validate JWT token
        private bool ValidateToken(string token, JwtSettings jwtSettings)
        {
            // Implement token validation logic here using a JWT library
            // For example, validate issuer, audience, expiration, etc.
            return true; // Replace with actual validation logic
        }
    }
}
*/