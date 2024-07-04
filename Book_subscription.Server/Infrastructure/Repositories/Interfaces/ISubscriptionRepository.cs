using Book_subscription.Server.Core.Entities;

namespace Book_subscription.Server.Infrastructure.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task AddSubscriptionAsync(Subscription subscription);
        Task RemoveSubscriptionAsync(int bookId, string userId);
        Task<Subscription> GetSubscriptionAsync(int bookId, string userId);
    }
}
