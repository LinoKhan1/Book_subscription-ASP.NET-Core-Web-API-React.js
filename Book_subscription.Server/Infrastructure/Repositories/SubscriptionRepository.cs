using System;
using System.Threading.Tasks;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Book_subscription.Server.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subscription>> GetUserSubscriptionsAsync(string userId)
        {
            return await _context.Subscriptions
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task<Subscription> GetSubscriptionAsync(int subscriptionId)
        {
            return await _context.Subscriptions.FindAsync(subscriptionId);
        }

        public async Task<Subscription> SubscribeAsync(Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return subscription;
        }

        public async Task UnsubscribeAsync(int subscriptionId)
        {
            var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }
    }
}
