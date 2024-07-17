using AutoMapper;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Book_subscription.Server.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserSubscriptions()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var subscriptions = await _subscriptionService.GetUserSubscriptionsAsync(userId);
            return Ok(subscriptions);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSubscription(int id)
        {
            var subscription = await _subscriptionService.GetSubscriptionAsync(id);
            if (subscription == null)
            {
                return NotFound();
            }
            return Ok(subscription);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Subscribe([FromBody] SubscribeDTO subscribeDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var subscription = await _subscriptionService.SubscribeAsync(userId, subscribeDto);
            return CreatedAtAction(nameof(GetSubscription), new { id = subscription.SubscriptionId }, subscription);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Unsubscribe(int id)
        {
            await _subscriptionService.UnsubscribeAsync(id);
            return NoContent();
        }
    }

}
