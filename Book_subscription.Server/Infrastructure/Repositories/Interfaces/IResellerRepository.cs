using Book_subscription.Server.Core.Entities;

namespace Book_subscription.Server.Infrastructure.Repositories.Interfaces
{
    public interface IResellerRepository
    {
        Task<Reseller> GetResellerByApiKeyAsync(string apiKey);
        Task<Reseller> AddResellerAsync(Reseller reseller);

    }
}
