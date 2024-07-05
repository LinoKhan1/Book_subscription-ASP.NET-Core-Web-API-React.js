using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;

namespace Book_subscription.Server.Core.Services
{
    public class ResellerService : IResellerService
    {
        private readonly IResellerRepository _resellerRepository;
        private readonly IApiKeyService _apiKeyService;

        public ResellerService(IResellerRepository resellerRepository, IApiKeyService apiKeyService)
        {
            _resellerRepository = resellerRepository;
            _apiKeyService = apiKeyService;
        }

        public async Task<string> RegisterResellerAsync(Reseller reseller)
        {
            // Add reseller to the database
            await _resellerRepository.AddResellerAsync(reseller);

            // Generate API key for the reseller
            var apiKey = await _apiKeyService.GenerateApiKeyAsync();
            return apiKey;
        }

        public async Task<Reseller> GetResellerByApiKeyAsync(string apiKey)
        {
            return await _resellerRepository.GetResellerByApiKeyAsync(apiKey);
        }

        
    }
}
