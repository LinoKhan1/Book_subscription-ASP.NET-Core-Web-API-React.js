using System.Threading.Tasks;
using Book_subscription.Server.Core.Entities;

namespace Book_subscription.Server.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Interface for managing subscriptions in the repository.
    /// </summary>
    public interface ISubscriptionRepository
    {
        /// <summary>
        /// Adds a new subscription to the repository.
        /// </summary>
        /// <param name="subscription">The subscription entity to add.</param>
        Task AddSubscriptionAsync(Subscription subscription);

        /// <summary>
        /// Removes a subscription from the repository.
        /// </summary>
        /// <param name="bookId">The identifier of the book.</param>
        /// <param name="userId">The identifier of the user.</param>
        Task RemoveSubscriptionAsync(int bookId, string userId);

        /// <summary>
        /// Retrieves a subscription from the repository based on book and user identifiers.
        /// </summary>
        /// <param name="bookId">The identifier of the book.</param>
        /// <param name="userId">The identifier of the user.</param>
        /// <returns>The subscription entity.</returns>
        Task<Subscription> GetSubscriptionAsync(int bookId, string userId);
    }
}
