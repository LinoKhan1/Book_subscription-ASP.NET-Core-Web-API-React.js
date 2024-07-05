using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Book_subscription.tests.Repositories
{
    public class SubscriptionRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public SubscriptionRepositoryTests()
        {
            // Use in-memory database for testing
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;
        }

        [Fact]
        public async Task AddSubscriptionAsync_ValidSubscription_AddsToDatabase()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new SubscriptionRepository(context);
                var subscription = new Subscription
                {
                    BookId = 1,
                    UserId = "user1",
                    SubscriptionDate = DateTime.Now
                };

                // Act
                await repository.AddSubscriptionAsync(subscription);
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new ApplicationDbContext(_options))
            {
                var savedSubscription = await context.Subscriptions.FirstOrDefaultAsync();
                Assert.NotNull(savedSubscription);
                Assert.Equal(1, savedSubscription.BookId);
                Assert.Equal("user1", savedSubscription.UserId);
            }
        }

        


        [Fact]
        public async Task RemoveSubscriptionAsync_ExistingSubscription_RemovesFromDatabase()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var initialSubscription = new Subscription
                {
                    BookId = 1,
                    UserId = "user1",
                    SubscriptionDate = DateTime.Now
                };
                context.Subscriptions.Add(initialSubscription);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new SubscriptionRepository(context);
                await repository.RemoveSubscriptionAsync(1, "user1");
                await context.SaveChangesAsync();
            }

            // Assert
            using (var context = new ApplicationDbContext(_options))
            {
                var deletedSubscription = await context.Subscriptions.FirstOrDefaultAsync();
                Assert.Null(deletedSubscription);
            }
        }

        [Fact]
        public async Task RemoveSubscriptionAsync_NonExistingSubscription_NoChanges()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new SubscriptionRepository(context);

                // Act
                await repository.RemoveSubscriptionAsync(1, "user1");
                await context.SaveChangesAsync();

                // Assert
                var deletedSubscription = await context.Subscriptions.FirstOrDefaultAsync();
                Assert.Null(deletedSubscription);
            }
        }

        [Fact]
        public async Task GetSubscriptionAsync_ExistingSubscription_ReturnsSubscription()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var initialSubscription = new Subscription
                {
                    BookId = 1,
                    UserId = "user1",
                    SubscriptionDate = DateTime.Now
                };
                context.Subscriptions.Add(initialSubscription);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new SubscriptionRepository(context);
                var subscription = await repository.GetSubscriptionAsync(1, "user1");

                // Assert
                Assert.NotNull(subscription);
                Assert.Equal(1, subscription.BookId);
                Assert.Equal("user1", subscription.UserId);
            }
        }

        [Fact]
        public async Task GetSubscriptionAsync_NonExistingSubscription_ReturnsNull()
        {
            // Arrange
            using (var context = new ApplicationDbContext(_options))
            {
                var repository = new SubscriptionRepository(context);

                // Act
                var subscription = await repository.GetSubscriptionAsync(1, "user1");

                // Assert
                Assert.Null(subscription);
            }
        }
    }
}
