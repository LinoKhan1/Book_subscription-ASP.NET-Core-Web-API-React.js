using AutoMapper;
using Book_subscription.Server.API.DTOs.SubscriptionDTOs;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_subscription.Server.API.Controllers
{
    /// <summary>
    /// Controller for managing subscriptions to books.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for SubscriptionController.
        /// </summary>
        /// <param name="subscriptionService">The subscription service instance.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint for subscribing to a book.
        /// </summary>
        /// <param name="subscriptionDTO">The subscription data.</param>
        /// <returns>ActionResult containing the subscribed subscriptionDTO.</returns>
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

        /// <summary>
        /// Endpoint for unsubscribing from a book.
        /// </summary>
        /// <param name="bookId">The ID of the book to unsubscribe from.</param>
        /// <returns>ActionResult indicating success or failure.</returns>
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

        /// <summary>
        /// Endpoint for retrieving subscription details for a book.
        /// </summary>
        /// <param name="bookId">The ID of the book.</param>
        /// <returns>ActionResult containing the subscription details.</returns>
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
