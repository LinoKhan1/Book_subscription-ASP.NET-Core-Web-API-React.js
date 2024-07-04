using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Book_subscription.Server.Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {

        private readonly ApplicationDbContext _context;

        public SubscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
        }

        public async Task RemoveSubscriptionAsync(int bookId, string userId)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.BookId == bookId && s.UserId == userId);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
            }
        }

        public async Task<Subscription> GetSubscriptionAsync(int bookId, string userId)
        {
            return await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.BookId == bookId && s.UserId == userId);
        }

    }
}
