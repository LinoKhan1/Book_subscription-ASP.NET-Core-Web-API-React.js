using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Book_subscription.Server.Core.Configurations;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Book_subscription.tests.Services.Unit
{
    public class ApiKeyServiceTests
    {
        private readonly ApiKeySettings _apiKeySettings;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public ApiKeyServiceTests()
        {
            _apiKeySettings = new ApiKeySettings { EncryptionKey = "testencryptionkey" };
            _mockContext = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        /*[Fact]
        public async Task GenerateApiKeyAsync_ShouldGenerateAndStoreApiKey()
        {
            // Arrange
            var service = new ApiKeyService(Options.Create(_apiKeySettings), _mockContext.Object, _mockUnitOfWork.Object);

            // Act
            var apiKey = await service.GenerateApiKeyAsync();

            // Assert
            Assert.NotNull(apiKey);
            _mockContext.Verify(c => c.ApiKeys.Add(It.IsAny<ApiKey>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public void HashApiKey_ShouldReturnCorrectHash()
        {
            // Arrange
            var service = new ApiKeyService(Options.Create(_apiKeySettings), _mockContext.Object, _mockUnitOfWork.Object);
            var apiKey = "testapikey";

            // Act
            var hashedApiKey = service.HashApiKey(apiKey);

            // Assert
            Assert.NotNull(hashedApiKey);
            Assert.NotEqual(apiKey, hashedApiKey); // Check that hash is not plain text
        }
/*
        [Fact]
        public async Task ValidateApiKeyAsync_ShouldReturnTrueForValidApiKey()
        {
            // Arrange
            var apiKey = "testapikey";
            var hashedApiKey = new ApiKeyService(Options.Create(_apiKeySettings), _mockContext.Object, _mockUnitOfWork.Object).HashApiKey(apiKey);
            _mockContext.Setup(c => c.ApiKeys.AnyAsync(k => k.Key == hashedApiKey)).ReturnsAsync(true);

            // Act
            var isValid = await new ApiKeyService(Options.Create(_apiKeySettings), _mockContext.Object, _mockUnitOfWork.Object).ValidateApiKeyAsync(apiKey);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public async Task ValidateApiKeyAsync_ShouldReturnFalseForInvalidApiKey()
        {
            // Arrange
            var apiKey = "invalidapikey";
            _mockContext.Setup(c => c.ApiKeys.AnyAsync(k => k.Key == It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var isValid = await new ApiKeyService(Options.Create(_apiKeySettings), _mockContext.Object, _mockUnitOfWork.Object).ValidateApiKeyAsync(apiKey);

            // Assert
            Assert.False(isValid);
        }
*/
    }
}
