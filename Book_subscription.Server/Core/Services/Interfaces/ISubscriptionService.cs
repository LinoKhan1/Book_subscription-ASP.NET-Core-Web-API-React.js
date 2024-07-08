using System.Threading.Tasks;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    /// <summary>
    /// Interface for managing subscriptions to books.
    /// </summary>
    public interface ISubscriptionService
    {
        /// <summary>
        /// Subscribes a user to a book.
        /// </summary>
        /// <param name="subscriptionDTO">The subscription DTO containing book and user identifiers.</param>
        /// <returns>The updated subscription DTO.</returns>
        Task<SubscriptionDTO> SubscribeAsync(SubscriptionDTO subscriptionDTO);

        /// <summary>
        /// Unsubscribes a user from a book.
        /// </summary>
        /// <param name="bookId">The identifier of the book to unsubscribe from.</param>
        /// <param name="userId">The identifier of the user.</param>
        Task UnsubscribeAsync(int bookId, string userId);

        /// <summary>
        /// Retrieves the subscription details for a book and user.
        /// </summary>
        /// <param name="bookId">The identifier of the book.</param>
        /// <param name="userId">The identifier of the user.</param>
        /// <returns>The subscription DTO.</returns>
        Task<SubscriptionDTO> GetSubscriptionAsync(int bookId, string userId);
    }
}
