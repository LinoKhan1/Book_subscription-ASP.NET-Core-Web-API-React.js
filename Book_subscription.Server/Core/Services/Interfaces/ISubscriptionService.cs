using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Entities;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDTO> SubscribeAsync(SubscriptionDTO subscriptionDTO);
        Task UnsubscribeAsync(int bookId, string userId);
        Task<SubscriptionDTO> GetSubscriptionAsync(int bookId, string userId);
    }
}
