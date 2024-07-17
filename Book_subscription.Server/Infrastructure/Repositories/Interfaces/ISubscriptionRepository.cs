using System.Threading.Tasks;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Entities;

namespace Book_subscription.Server.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Interface for managing subscriptions in the repository.
    /// </summary>
    public interface ISubscriptionRepository
    {
        
        Task<IEnumerable<Subscription>> GetUserSubscriptionsAsync(string userId);
        Task<Subscription> GetSubscriptionAsync(int subscriptionId);
        Task<Subscription> SubscribeAsync(Subscription subscription);
        Task UnsubscribeAsync(int subscriptionId);
    }
}
