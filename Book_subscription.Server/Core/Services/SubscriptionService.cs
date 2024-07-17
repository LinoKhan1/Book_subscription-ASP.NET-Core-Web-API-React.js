using AutoMapper;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Book_subscription.Server.Core.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubscriptionDTO>> GetUserSubscriptionsAsync(string userId)
        {
            var subscriptions = await _subscriptionRepository.GetUserSubscriptionsAsync(userId);
            return _mapper.Map<IEnumerable<SubscriptionDTO>>(subscriptions);
        }

        public async Task<SubscriptionDTO> GetSubscriptionAsync(int subscriptionId)
        {
            var subscription = await _subscriptionRepository.GetSubscriptionAsync(subscriptionId);
            return _mapper.Map<SubscriptionDTO>(subscription);
        }

        public async Task<SubscriptionDTO> SubscribeAsync(string userId, SubscribeDTO subscribeDto)
        {
            var subscription = new Subscription
            {
                UserId = userId,
                BookId = subscribeDto.BookId,
                SubscriptionDate = DateTime.UtcNow
            };
            var createdSubscription = await _subscriptionRepository.SubscribeAsync(subscription);
            return _mapper.Map<SubscriptionDTO>(createdSubscription);
        }

        public async Task UnsubscribeAsync(int subscriptionId)
        {
            await _subscriptionRepository.UnsubscribeAsync(subscriptionId);
        }
    }
}
