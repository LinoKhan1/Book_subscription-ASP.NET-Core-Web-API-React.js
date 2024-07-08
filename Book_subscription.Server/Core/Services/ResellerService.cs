using AutoMapper;
using Book_subscription.Server.API.DTOs.ResellerDTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;

namespace Book_subscription.Server.Core.Services
{

    /// <summary>
    /// Service class for managing reseller operations.
    /// </summary>
    public class ResellerService : IResellerService
    {
        private readonly IResellerRepository _resellerRepository;
        private readonly IApiKeyService _apiKeyService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ResellerService> _logger;

        public ResellerService(IResellerRepository resellerRepository, IApiKeyService apiKeyService, IUnitOfWork unitOfWork, IMapper mapper, ILogger<ResellerService> logger)
        {
            _resellerRepository = resellerRepository;
            _apiKeyService = apiKeyService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Registers a new reseller asynchronously.
        /// </summary>
        /// <param name="resellerDTO">The reseller DTO containing reseller information.</param>
        /// <returns>The registered reseller DTO with API key.</returns>
        public async Task<ResellerDTO> RegisterResellerAsync(ResellerDTO resellerDTO)
        {
            try
            {
                var resellerEntity = _mapper.Map<Reseller>(resellerDTO);

                // Generate an API key
                var apiKey = await _apiKeyService.GenerateApiKeyAsync();
                if (apiKey != null)
                {
                    resellerEntity.ApiKey = _apiKeyService.HashApiKey(apiKey);
                }

                await _resellerRepository.AddResellerAsync(resellerEntity);
                await _unitOfWork.CompleteAsync();

                // Map the entity back to DTO and include the plain API key
                var result = _mapper.Map<ResellerDTO>(resellerEntity);
                result.ApiKey = apiKey;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering reseller.");
                throw; // Propagate the exception for global error handling middleware
            }
        }

        /// <summary>
        /// Retrieves a reseller entity by API key asynchronously.
        /// </summary>
        /// <param name="apiKey">The API key of the reseller to retrieve.</param>
        /// <returns>The reseller entity associated with the API key.</returns>
        public async Task<Reseller> GetResellerByApiKeyAsync(string apiKey)
        {
            try
            {
                return await _resellerRepository.GetResellerByApiKeyAsync(apiKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving reseller by API key.");
                throw; // Propagate the exception for global error handling middleware
            }
        }

    }
}
