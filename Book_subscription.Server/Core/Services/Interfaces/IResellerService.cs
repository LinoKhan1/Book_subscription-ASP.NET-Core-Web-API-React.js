using Book_subscription.Server.API.DTOs.ResellerDTOs;
using Book_subscription.Server.Core.Entities;
using System.Threading.Tasks;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    /// <summary>
    /// Interface for managing reseller operations.
    /// </summary>
    public interface IResellerService
    {
        /// <summary>
        /// Registers a new reseller asynchronously.
        /// </summary>
        /// <param name="resellerDTO">The reseller DTO containing reseller information.</param>
        /// <returns>The registered reseller DTO.</returns>
        Task<ResellerDTO> RegisterResellerAsync(ResellerDTO resellerDTO);

        /// <summary>
        /// Retrieves a reseller entity by API key asynchronously.
        /// </summary>
        /// <param name="apiKey">The API key of the reseller to retrieve.</param>
        /// <returns>The reseller entity associated with the API key.</returns>
        Task<Reseller> GetResellerByApiKeyAsync(string apiKey);
    }
}
