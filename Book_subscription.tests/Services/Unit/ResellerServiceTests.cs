using AutoMapper;
using Book_subscription.Server.API.DTOs.ResellerDTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Book_subscription.tests.Services.Unit
{
    public class ResellerServiceTests
    {
        private readonly IResellerService _resellerService;
        private readonly Mock<IResellerRepository> _mockResellerRepository;
        private readonly Mock<IApiKeyService> _mockApiKeyService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public ResellerServiceTests()
        {
            _mockResellerRepository = new Mock<IResellerRepository>();
            _mockApiKeyService = new Mock<IApiKeyService>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();

            _resellerService = new ResellerService(
                _mockResellerRepository.Object,
                _mockApiKeyService.Object,
                _mockUnitOfWork.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task RegisterResellerAsync_ShouldRegisterResellerAndReturnResellerDTOWithApiKey()
        {
            // Arrange
            var resellerDTO = new ResellerDTO
            {
                Name = "Test Reseller"
            };

            var apiKey = "generated-api-key";
            var hashedApiKey = "hashed-api-key";

            _mockApiKeyService.Setup(s => s.GenerateApiKeyAsync()).ReturnsAsync(apiKey);
            _mockApiKeyService.Setup(s => s.HashApiKey(apiKey)).Returns(hashedApiKey);

            var expectedResellerEntity = new Reseller
            {
                Name = resellerDTO.Name,
                ApiKey = hashedApiKey // Ensure hash is set
            };

            var expectedResellerDTO = new ResellerDTO
            {
                Name = resellerDTO.Name,
                ApiKey = apiKey // Ensure plain API key is returned
            };

            _mockMapper.Setup(m => m.Map<Reseller>(resellerDTO)).Returns(expectedResellerEntity);
            _mockMapper.Setup(m => m.Map<ResellerDTO>(expectedResellerEntity)).Returns(expectedResellerDTO);

            _mockResellerRepository.Setup(r => r.AddResellerAsync(It.IsAny<Reseller>())).ReturnsAsync(expectedResellerEntity);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _resellerService.RegisterResellerAsync(resellerDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResellerDTO.Name, result.Name);
            Assert.Equal(expectedResellerDTO.ApiKey, result.ApiKey);
            _mockResellerRepository.Verify(r => r.AddResellerAsync(expectedResellerEntity), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task GetResellerByApiKeyAsync_ShouldReturnResellerForValidApiKey()
        {
            // Arrange
            var apiKey = "valid-api-key";
            var expectedReseller = new Reseller
            {
                ResellerId = 1,
                Name = "Test Reseller",
                ApiKey = apiKey
            };

            _mockResellerRepository.Setup(r => r.GetResellerByApiKeyAsync(apiKey)).ReturnsAsync(expectedReseller);

            // Act
            var result = await _resellerService.GetResellerByApiKeyAsync(apiKey);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedReseller.ResellerId, result.ResellerId);
            Assert.Equal(expectedReseller.Name, result.Name);
            Assert.Equal(expectedReseller.ApiKey, result.ApiKey);
        }

        [Fact]
        public async Task GetResellerByApiKeyAsync_ShouldReturnNullForInvalidApiKey()
        {
            // Arrange
            var invalidApiKey = "invalid-api-key";

            _mockResellerRepository.Setup(r => r.GetResellerByApiKeyAsync(invalidApiKey)).ReturnsAsync((Reseller)null);

            // Act
            var result = await _resellerService.GetResellerByApiKeyAsync(invalidApiKey);

            // Assert
            Assert.Null(result);
        }
    }
}
