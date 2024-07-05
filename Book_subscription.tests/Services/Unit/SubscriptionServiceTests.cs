using AutoMapper;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_subscription.tests.Services.Unit
{
    public class SubscriptionServiceTests
    {
        private readonly SubscriptionService _subscriptionService;
        private readonly Mock<ISubscriptionRepository> _mockSubscriptionRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public SubscriptionServiceTests()
        {
            _mockSubscriptionRepository = new Mock<ISubscriptionRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();  
            _mockMapper = new Mock<IMapper>();

            _subscriptionService = new SubscriptionService(
               _mockSubscriptionRepository.Object,
               _mockUnitOfWork.Object,
               _mockMapper.Object);

        }

        [Fact]
        public async Task SubscribeAsync_NoExistingSubscription_Success()
        {
            // Arrange
            var subscriptionDTO = new SubscriptionDTO
            {
                BookId = 1,
                UserId = "user1"
            };

            var subscriptionEntity = new Subscription
            {
                BookId = subscriptionDTO.BookId,
                UserId = subscriptionDTO.UserId,
                SubscriptionDate = DateTime.UtcNow
            };

            // Mock IMapper to return subscriptionEntity when mapping from SubscriptionDTO
            _mockMapper.Setup(m => m.Map<Subscription>(subscriptionDTO)).Returns(subscriptionEntity);

            // Mock SubscriptionRepository to return null for GetSubscriptionAsync
            _mockSubscriptionRepository.Setup(r => r.GetSubscriptionAsync(subscriptionDTO.BookId, subscriptionDTO.UserId))
                                       .ReturnsAsync((Subscription)null);

            // Act
            var result = await _subscriptionService.SubscribeAsync(subscriptionDTO);

            // Assert
            Assert.NotNull(result); // Ensure that result is not null
            Assert.Equal(subscriptionDTO.BookId, result.BookId); // Validate BookId
            Assert.Equal(subscriptionDTO.UserId, result.UserId); // Validate UserId

            // Verify that AddSubscriptionAsync and CompleteAsync were called
            _mockSubscriptionRepository.Verify(r => r.AddSubscriptionAsync(It.IsAny<Subscription>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }


        [Fact]
        public async Task SubscribeAsync_DuplicateSubscription_ThrowsException()
        {
            // Arrange
            var subscriptionDTO = new SubscriptionDTO
            {
                BookId = 1,
                UserId = "user1"
            };

            _mockSubscriptionRepository.Setup(r => r.GetSubscriptionAsync(subscriptionDTO.BookId, subscriptionDTO.UserId))
                .ReturnsAsync(new Subscription()); // Simulate existing subscription

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _subscriptionService.SubscribeAsync(subscriptionDTO);
            });

            Assert.Equal("User is already subscribed to this book.", exception.Message);
            _mockSubscriptionRepository.Verify(r => r.AddSubscriptionAsync(It.IsAny<Subscription>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }



        [Fact]
        public async Task UnsubscribeAsync_Success()
        {
            // Arrange
            int bookId = 1;
            string userId = "user1";

            // Act
            await _subscriptionService.UnsubscribeAsync(bookId, userId);

            // Assert
            _mockSubscriptionRepository.Verify(r => r.RemoveSubscriptionAsync(bookId, userId), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task GetSubscriptionAsync_ReturnsSubscription()
        {
            // Arrange
            int bookId = 1;
            string userId = "user1";

            var subscriptionEntity = new Subscription
            {
                BookId = bookId,
                UserId = userId,
                SubscriptionDate = DateTime.UtcNow
            };

            var subscriptionDTO = new SubscriptionDTO
            {
                BookId = bookId,
                UserId = userId
            };

            _mockSubscriptionRepository.Setup(r => r.GetSubscriptionAsync(bookId, userId)).ReturnsAsync(subscriptionEntity);
            _mockMapper.Setup(m => m.Map<SubscriptionDTO>(subscriptionEntity)).Returns(subscriptionDTO);

            // Act
            var result = await _subscriptionService.GetSubscriptionAsync(bookId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(subscriptionDTO.BookId, result.BookId);
            Assert.Equal(subscriptionDTO.UserId, result.UserId);
        }

    }
}
