using Book_subscription.Server.Core.Entities;
using System.Threading.Tasks;

namespace Book_subscription.Server.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for managing reseller entities.
    /// </summary>
    public interface IResellerRepository
    {
        /// <summary>
        /// Retrieves a reseller entity by its API key asynchronously.
        /// </summary>
        /// <param name="apiKey">The API key of the reseller to retrieve.</param>
        /// <returns>The reseller entity associated with the API key.</returns>
        Task<Reseller> GetResellerByApiKeyAsync(string apiKey);

        /// <summary>
        /// Adds a new reseller asynchronously.
        /// </summary>
        /// <param name="reseller">The reseller entity to add.</param>
        /// <returns>The added reseller entity.</returns>
        Task<Reseller> AddResellerAsync(Reseller reseller);
    }
}
