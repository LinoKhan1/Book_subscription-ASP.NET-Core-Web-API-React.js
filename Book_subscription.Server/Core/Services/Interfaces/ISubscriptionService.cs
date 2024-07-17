using System.Threading.Tasks;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDTO>> GetUserSubscriptionsAsync(string userId);
        Task<SubscriptionDTO> GetSubscriptionAsync(int subscriptionId);
        Task<SubscriptionDTO> SubscribeAsync(string userId, SubscribeDTO subscribeDto);
        Task UnsubscribeAsync(int subscriptionId);
    }
}
