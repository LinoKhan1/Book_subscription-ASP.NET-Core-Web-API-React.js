using AutoMapper;
using Book_subscription.Server.API.Controllers;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Book_subscription.tests.Controllers.Unit
{
    public class SubscriptionControllerTests
    {
        private readonly Mock<ISubscriptionService> _mockSubscriptionService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SubscriptionController _controller;

        public SubscriptionControllerTests()
        {
            _mockSubscriptionService = new Mock<ISubscriptionService>();
            _mockMapper = new Mock<IMapper>();
            _controller = new SubscriptionController(_mockSubscriptionService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Subscribe_Success_ReturnsOk()
        {
            // Arrange
            var subscriptionDTO = new SubscriptionDTO { BookId = 1, UserId = "user1" };
            var subscriptionEntity = new Subscription { BookId = 1, UserId = "user1", SubscriptionDate = DateTime.UtcNow };
            var subscriptionDTOReturned = new SubscriptionDTO { BookId = 1, UserId = "user1" };

            _mockMapper.Setup(m => m.Map<Subscription>(subscriptionDTO)).Returns(subscriptionEntity);
            _mockSubscriptionService.Setup(s => s.SubscribeAsync(subscriptionDTO)).ReturnsAsync(subscriptionDTOReturned);

            // Act
            var result = await _controller.Subscribe(subscriptionDTO) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(subscriptionDTOReturned, result.Value);
        }

        [Fact]
        public async Task Subscribe_DuplicateSubscription_ReturnsBadRequest()
        {
            // Arrange
            var subscriptionDTO = new SubscriptionDTO { BookId = 1, UserId = "user1" };

            _mockSubscriptionService.Setup(s => s.SubscribeAsync(subscriptionDTO))
                .ThrowsAsync(new InvalidOperationException("User is already subscribed to this book."));

            // Act
            var result = await _controller.Subscribe(subscriptionDTO) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("User is already subscribed to this book.", result.Value);
        }

        [Fact]
        public async Task Unsubscribe_Success_ReturnsOk()
        {
            // Arrange
            int bookId = 1;
            var userId = "user1";
            var mockHttpContext = new Mock<HttpContext>();
            var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();

            mockClaimsPrincipal.Setup(c => c.FindFirst(ClaimTypes.NameIdentifier)).Returns(new Claim(ClaimTypes.NameIdentifier, userId));
            mockHttpContext.SetupGet(h => h.User).Returns(mockClaimsPrincipal.Object);

            _controller.ControllerContext = new ControllerContext { HttpContext = mockHttpContext.Object };

            // Act
            var result = await _controller.Unsubscribe(bookId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Unsubscribed successfully.", result.Value);
        }

        [Fact]
        public async Task Unsubscribe_NotAuthenticated_ReturnsBadRequest()
        {
            // Arrange
            int bookId = 1;

            // Setup mock HttpContext with a principal that has no identity
            var mockIdentity = new Mock<ClaimsIdentity>();
            var mockPrincipal = new Mock<ClaimsPrincipal>(mockIdentity.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(h => h.User).Returns(mockPrincipal.Object);

            // Setup ControllerContext with mock HttpContext
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            };

            // Act
            var result = await _controller.Unsubscribe(bookId) as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }


        [Fact]
        public async Task GetSubscription_Success_ReturnsOk()
        {
            // Arrange
            int bookId = 1;
            var userId = "user1";
            var subscriptionDTO = new SubscriptionDTO { BookId = 1, UserId = "user1" };
            var mockHttpContext = new Mock<HttpContext>();
            var mockClaimsPrincipal = new Mock<ClaimsPrincipal>();

            mockClaimsPrincipal.Setup(c => c.FindFirst(ClaimTypes.NameIdentifier)).Returns(new Claim(ClaimTypes.NameIdentifier, userId));
            mockHttpContext.SetupGet(h => h.User).Returns(mockClaimsPrincipal.Object);
            _mockSubscriptionService.Setup(s => s.GetSubscriptionAsync(bookId, userId)).ReturnsAsync(subscriptionDTO);

            _controller.ControllerContext = new ControllerContext { HttpContext = mockHttpContext.Object };

            // Act
            var result = await _controller.GetSubscription(bookId) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(subscriptionDTO, result.Value);
        }

        [Fact]
        public async Task GetSubscription_NotAuthenticated_ReturnsBadRequest()
        {
            // Arrange
            int bookId = 1;

            // Setup mock HttpContext with a principal that has no identity
            var mockIdentity = new Mock<ClaimsIdentity>();
            var mockPrincipal = new Mock<ClaimsPrincipal>(mockIdentity.Object);
            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.SetupGet(h => h.User).Returns(mockPrincipal.Object);

            // Setup ControllerContext with mock HttpContext
            var controller = new SubscriptionController(_mockSubscriptionService.Object, _mockMapper.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockHttpContext.Object
                }
            };

            // Act
            var result = await controller.GetSubscription(bookId) as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }



    }
}
