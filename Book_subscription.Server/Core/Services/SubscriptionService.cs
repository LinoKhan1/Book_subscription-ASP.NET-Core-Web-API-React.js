using AutoMapper;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;

namespace Book_subscription.Server.Core.Services
{
    public class SubscriptionService : ISubscriptionService
    {

        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SubscriptionDTO> SubscribeAsync(SubscriptionDTO subscriptionDTO)
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
        public async Task UnsubscribeAsync(int bookId, string userId)
        {
            await _subscriptionRepository.RemoveSubscriptionAsync(bookId, userId);
            await _unitOfWork.CompleteAsync();

        }

        public async Task<SubscriptionDTO> GetSubscriptionAsync(int bookId, string userId)
        {
            var subscription = await _subscriptionRepository.GetSubscriptionAsync(bookId, userId);
            return _mapper.Map<SubscriptionDTO>(subscription);
        }


    }
}
