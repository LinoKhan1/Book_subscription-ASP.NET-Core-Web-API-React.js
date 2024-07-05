using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Book_subscription.Server.Infrastructure.Repositories
{
    public class ResellerRepository : IResellerRepository
    {

        private readonly ApplicationDbContext _context;

        public ResellerRepository(ApplicationDbContext context)
        {

            _context = context;
        }

        public async Task<Reseller> GetResellerByApiKeyAsync(string apiKey)
        {
            return await _context.Resellers.FirstOrDefaultAsync(r => r.ApiKey == apiKey);

        }
        public async Task<Reseller> AddResellerAsync(Reseller reseller)
        {
            _context.Resellers.Add(reseller);
            await _context.SaveChangesAsync();
            return reseller;
        }

    }
}
