using System;
using System.Threading.Tasks;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Import the ILogger namespace

namespace Book_subscription.Server.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SubscriptionRepository> _logger; // Logger instance

        public SubscriptionRepository(ApplicationDbContext context, ILogger<SubscriptionRepository> logger)
        {
            _context = context;
            _logger = logger; // Injected logger
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            try
            {
                await _context.Subscriptions.AddAsync(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding subscription."); // Log the error
                throw; // Re-throw the exception to propagate it
            }
        }

        public async Task RemoveSubscriptionAsync(int bookId, string userId)
        {
            try
            {
                var subscription = await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.BookId == bookId && s.UserId == userId);

                if (subscription != null)
                {
                    _context.Subscriptions.Remove(subscription);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing subscription."); // Log the error
                throw; // Re-throw the exception to propagate it
            }
        }

        public async Task<Subscription> GetSubscriptionAsync(int bookId, string userId)
        {
            try
            {
                return await _context.Subscriptions
                    .FirstOrDefaultAsync(s => s.BookId == bookId && s.UserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving subscription."); // Log the error
                throw; // Re-throw the exception to propagate it
            }
        }
    }
}
