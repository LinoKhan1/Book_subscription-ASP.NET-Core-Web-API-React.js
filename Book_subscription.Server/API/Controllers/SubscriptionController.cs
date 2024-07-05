using AutoMapper;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_subscription.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;

        public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionDTO subscriptionDTO)
        {
            try
            {
                var result = await _subscriptionService.SubscribeAsync(subscriptionDTO);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message );
            }
        }

        [HttpDelete("unsubscribe/{bookId}")]
        public async Task<IActionResult> Unsubscribe(int bookId)
        {
            // Get UserId from claims or context
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                if (userId != null) 
                {
                    await _subscriptionService.UnsubscribeAsync(bookId, userId);
                    return Ok("Unsubscribed successfully.");
                }
                else
                {
                    return BadRequest();
                }
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("subscription/{bookId}")]
        public async Task<IActionResult> GetSubscription(int bookId)
        {
            // Get UserId from claims or context
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                if(userId != null)
                {
                    var result = await _subscriptionService.GetSubscriptionAsync(bookId, userId);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
