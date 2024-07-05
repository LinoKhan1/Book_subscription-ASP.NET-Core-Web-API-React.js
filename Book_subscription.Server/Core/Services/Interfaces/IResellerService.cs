using Book_subscription.Server.Core.Entities;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    public interface IResellerService
    {

        Task<string> RegisterResellerAsync(Reseller reseller);
        Task<Reseller> GetResellerByApiKeyAsync(string apiKey);


    }
}
