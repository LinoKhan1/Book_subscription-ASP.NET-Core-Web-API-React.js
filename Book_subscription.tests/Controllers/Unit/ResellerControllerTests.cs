using AutoMapper;
using Book_subscription.Server.API.Controllers;
using Book_subscription.Server.API.DTOs.ResellerDTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Book_subscription.tests.Controllers.Unit
{
    public class ResellerControllerTests
    {
        private readonly Mock<IResellerService> _mockResellerService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ResellerController _controller;

        public ResellerControllerTests()
        {
            _mockResellerService = new Mock<IResellerService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ResellerController(_mockResellerService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task RegisterReseller_ValidInput_ReturnsOk()
        {
            // Arrange
            var resellerDTO = new ResellerDTO { ResellerId = 1, Name = "Test Reseller", ApiKey="Test-api-key" };
            var resellerEntity = new Reseller { ResellerId = 1, Name = "Test Reseller", ApiKey = "Test-api-key" };
            _mockMapper.Setup(m => m.Map<Reseller>(resellerDTO)).Returns(resellerEntity);
            _mockResellerService.Setup(s => s.RegisterResellerAsync(resellerDTO)).ReturnsAsync(resellerDTO);

            // Act
            var result = await _controller.RegisterReseller(resellerDTO) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task RegisterReseller_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Key", "Error Message");

            // Act
            var result = await _controller.RegisterReseller(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetResellerByApiKey_ValidApiKey_ReturnsOk()
        {
            // Arrange
            var apiKey = "valid-api-key";
            var resellerDTO = new ResellerDTO { ResellerId = 1, Name = "Test Reseller", ApiKey = "Test-api-key" };
            var resellerEntity = new Reseller { ResellerId = 1, Name = "Test Reseller", ApiKey = "Test-api-key" };
            _mockResellerService.Setup(s => s.GetResellerByApiKeyAsync(apiKey)).ReturnsAsync(resellerEntity);
            _mockMapper.Setup(m => m.Map<ResellerDTO>(resellerEntity)).Returns(resellerDTO);

            // Act
            // Act
            var result = await _controller.GetResellerByApiKey(apiKey) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            // Cast the value to a dynamic object
            dynamic returnedObject = result.Value;

            // Access the ResellerDTO property
            var returnedResellerDTO = returnedObject.Reseller as ResellerDTO;

            // Assert the apiKey in returnedResellerDTO or other properties as needed
            Assert.Equal(apiKey, returnedResellerDTO.ApiKey);
        }

        [Fact]
        public async Task GetResellerByApiKey_InvalidApiKey_ReturnsNotFound()
        {
            // Arrange
            var apiKey = "invalid-api-key";
            _mockResellerService.Setup(s => s.GetResellerByApiKeyAsync(apiKey)).ReturnsAsync((Reseller)null);

            // Act
            var result = await _controller.GetResellerByApiKey(apiKey);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
