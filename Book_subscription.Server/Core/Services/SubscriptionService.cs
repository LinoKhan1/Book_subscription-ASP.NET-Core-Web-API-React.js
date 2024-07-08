using AutoMapper;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;

namespace Book_subscription.Server.Core.Services
{

    /// <summary>
    /// Service class for managing subscriptions to books.
    /// </summary>
    public class SubscriptionService : ISubscriptionService
    {

        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<SubscriptionService> _logger;


        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<SubscriptionService> logger)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Subscribes a user to a book.
        /// </summary>
        /// <param name="subscriptionDTO">The subscription DTO containing book and user identifiers.</param>
        /// <returns>The updated subscription DTO.</returns>
        public async Task<SubscriptionDTO> SubscribeAsync(SubscriptionDTO subscriptionDTO)
        {
            try
            {
                // Check if the subscription already exists
                var existingSubscription = await _subscriptionRepository.GetSubscriptionAsync(subscriptionDTO.BookId, subscriptionDTO.UserId);
                if (existingSubscription != null)
                {
                    throw new InvalidOperationException("User is already subscribed to this book.");
                }

                // Map DTO to Entity
                var subscription = _mapper.Map<Subscription>(subscriptionDTO);
                subscription.SubscriptionDate = DateTime.UtcNow;

                // Add subscription
                await _subscriptionRepository.AddSubscriptionAsync(subscription);
                await _unitOfWork.CompleteAsync();

                // Map Entity back to DTO
                return _mapper.Map<SubscriptionDTO>(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while subscribing user to book.");
                throw;
            }
        }

        /// <summary>
        /// Unsubscribes a user from a book.
        /// </summary>
        /// <param name="bookId">The identifier of the book to unsubscribe from.</param>
        /// <param name="userId">The identifier of the user.</param>
        public async Task UnsubscribeAsync(int bookId, string userId)
        {
            try
            {
                await _subscriptionRepository.RemoveSubscriptionAsync(bookId, userId);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while unsubscribing user from book.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves the subscription details for a book and user.
        /// </summary>
        /// <param name="bookId">The identifier of the book.</param>
        /// <param name="userId">The identifier of the user.</param>
        /// <returns>The subscription DTO.</returns>
        public async Task<SubscriptionDTO> GetSubscriptionAsync(int bookId, string userId)
        {
            try
            {
                var subscription = await _subscriptionRepository.GetSubscriptionAsync(bookId, userId);
                return _mapper.Map<SubscriptionDTO>(subscription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving subscription details.");
                throw;
            }
        }

    }
}
