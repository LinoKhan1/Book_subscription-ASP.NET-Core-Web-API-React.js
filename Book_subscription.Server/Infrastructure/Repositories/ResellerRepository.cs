using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Book_subscription.Server.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for managing reseller entities.
    /// </summary>

    public class ResellerRepository : IResellerRepository
    {

        private readonly ApplicationDbContext _context;

        public ResellerRepository(ApplicationDbContext context)
        {

            _context = context;
        }

        /// <summary>
        /// Retrieves a reseller entity by its API key asynchronously.
        /// </summary>
        /// <param name="apiKey">The API key of the reseller to retrieve.</param>
        /// <returns>The reseller entity associated with the API key.</returns>
        public async Task<Reseller> GetResellerByApiKeyAsync(string apiKey)
        {
            return await _context.Resellers.FirstOrDefaultAsync(r => r.ApiKey == apiKey);

        }
        /// <summary>
        /// Adds a new reseller asynchronously.
        /// </summary>
        /// <param name="reseller">The reseller entity to add.</param>
        /// <returns>The added reseller entity.</returns>
        public async Task<Reseller> AddResellerAsync(Reseller reseller)
        {
            _context.Resellers.Add(reseller);
            await _context.SaveChangesAsync();
            return reseller;
        }

    }
}
