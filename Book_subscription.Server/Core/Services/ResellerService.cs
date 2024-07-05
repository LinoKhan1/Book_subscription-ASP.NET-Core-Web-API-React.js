using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;

namespace Book_subscription.Server.Core.Services
{
    public class ResellerService : IResellerService
    {
        private readonly IResellerRepository _resellerRepository;

        public ResellerService(IResellerRepository resellerRepository)
        {
            _resellerRepository = resellerRepository;
        }

        public async Task<Reseller> RegisterResellerAsync(string name)
        {
            var apiKey = GenerateApiKey();
            var reseller = new Reseller
            {
                Name = name,
                ApiKey = apiKey
            };

            return await _resellerRepository.AddResellerAsync(reseller);
        }

        public async Task<Reseller> GetResellerByApiKeyAsync(string apiKey)
        {
            return await _resellerRepository.GetResellerByApiKeyAsync(apiKey);
        }

        private string GenerateApiKey()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
