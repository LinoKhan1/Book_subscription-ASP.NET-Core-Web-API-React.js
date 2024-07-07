using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_subscription.tests.Repositories
{
    public class ResellerRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ResellerRepository _repository;

        public ResellerRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _repository = new ResellerRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        [Fact]
        public async Task GetResellerByApiKeyAsync_ValidApiKey_ReturnsReseller()
        {
            // Arrange
            var reseller = new Reseller { ResellerId = 1, Name = "Test Reseller", ApiKey = "valid-api-key" };
            await _dbContext.Resellers.AddAsync(reseller);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetResellerByApiKeyAsync("valid-api-key");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reseller.ResellerId, result.ResellerId);
            Assert.Equal(reseller.Name, result.Name);
            Assert.Equal(reseller.ApiKey, result.ApiKey);
        }

        [Fact]
        public async Task GetResellerByApiKeyAsync_InvalidApiKey_ReturnsNull()
        {
            // Arrange
            var reseller = new Reseller { ResellerId = 1, Name = "Test Reseller", ApiKey = "valid-api-key" };
            await _dbContext.Resellers.AddAsync(reseller);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _repository.GetResellerByApiKeyAsync("invalid-api-key");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddResellerAsync_ValidReseller_AddsToDatabase()
        {
            // Arrange
            var reseller = new Reseller { ResellerId = 1, Name = "Test Reseller", ApiKey = "valid-api-key" };

            // Act
            var addedReseller = await _repository.AddResellerAsync(reseller);

            // Assert
            var result = await _dbContext.Resellers.FindAsync(addedReseller.ResellerId);
            Assert.NotNull(result);
            Assert.Equal(reseller.ResellerId, result.ResellerId);
            Assert.Equal(reseller.Name, result.Name);
            Assert.Equal(reseller.ApiKey, result.ApiKey);
        }
    }
}
