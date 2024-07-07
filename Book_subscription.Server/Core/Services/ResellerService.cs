using AutoMapper;
using Book_subscription.Server.API.DTOs.ResellerDTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;

namespace Book_subscription.Server.Core.Services
{
    public class ResellerService : IResellerService
    {
        private readonly IResellerRepository _resellerRepository;
        private readonly IApiKeyService _apiKeyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResellerService(IResellerRepository resellerRepository, IApiKeyService apiKeyService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _resellerRepository = resellerRepository;
            _apiKeyService = apiKeyService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResellerDTO> RegisterResellerAsync(ResellerDTO resellerDTO)
        {
            var resellerEntity = _mapper.Map<Reseller>(resellerDTO);

            // Generate an API key
            var apiKey = await _apiKeyService.GenerateApiKeyAsync();
            if (apiKey != null)
            {
                resellerEntity.ApiKey =  _apiKeyService.HashApiKey(apiKey);

            }
            await _resellerRepository.AddResellerAsync(resellerEntity);
            await _unitOfWork.CompleteAsync();

            // Map the entity back to DTO and include the plain API key
            var result = _mapper.Map<ResellerDTO>(resellerEntity);
            result.ApiKey = apiKey;
            return result;
            
        }

        public async Task<Reseller> GetResellerByApiKeyAsync(string apiKey)
        {
            return await _resellerRepository.GetResellerByApiKeyAsync(apiKey);
        }

        

        
    }
}
