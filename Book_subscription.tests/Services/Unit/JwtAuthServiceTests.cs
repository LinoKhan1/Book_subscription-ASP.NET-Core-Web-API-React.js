using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Book_subscription.Server.Core.Configurations;
using Book_subscription.Server.Core.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Xunit;

namespace Book_subscription.tests.Services.Unit
{
    public class JwtAuthServiceTests
    {
        private readonly JwtAuthService _jwtAuthService;

        public JwtAuthServiceTests()
        {
            // Mock IOptions<JwtSettings>
            var jwtSettings = new JwtSettings
            {
                Key = "4756abREang9m7J+PTV19Qasdf9PFnabCdJFgJadgj0=",
                Issuer = "localhost",
                Audience = "localhost",
                ExpireHours = 24
            };
            var options = Options.Create(jwtSettings);

            _jwtAuthService = new JwtAuthService(options);
        }

        [Fact]
        public async Task GenerateJwtTokenAsync_ValidParameters_ReturnsToken()
        {
            // Arrange
            string userId = "test_user_id";
            string userName = "test_user_name";

            // Act
            var token = await _jwtAuthService.GenerateJwtTokenAsync(userId, userName);

            // Assert
            Assert.NotNull(token);
            Assert.True(token.Length > 0);

            // Additional assertions if needed
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);

            Assert.Equal(userId, securityToken.Subject);
            Assert.Equal(userName, securityToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value);
        }

        [Fact]
        public async Task VerifyJwtTokenAsync_ValidToken_ReturnsTrue()
        {
            // Arrange
            string token = await _jwtAuthService.GenerateJwtTokenAsync("test_user_id", "test_user_name");

            // Act
            var result = await _jwtAuthService.VerifyJwtTokenAsync(token);

            // Assert
            Assert.True(result);
        }




        // Add more test methods for other scenarios as described

    }
}
