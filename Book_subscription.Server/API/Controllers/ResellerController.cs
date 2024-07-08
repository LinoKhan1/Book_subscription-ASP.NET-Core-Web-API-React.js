using AutoMapper;
using Book_subscription.Server.API.DTOs.ResellerDTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_subscription.Server.API.Controllers
{
    /// <summary>
    /// API controller for managing resellers.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ResellerController : ControllerBase
    {
        private readonly IResellerService _resellerService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResellerController"/> class.
        /// </summary>
        /// <param name="resellerService">The reseller service.</param>
        /// <param name="mapper">The mapper instance.</param>
        public ResellerController(IResellerService resellerService, IMapper mapper)
        {
            _resellerService = resellerService;
            _mapper = mapper;
        }

        /// <summary>
        /// Registers a new reseller.
        /// </summary>
        /// <param name="resellerDTO">The data for the reseller to register.</param>
        /// <returns>An IActionResult indicating the result of the registration operation.</returns>

        [HttpPost("register")]
        public async Task<IActionResult> RegisterReseller([FromBody] ResellerDTO resellerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var registeredReseller = await _resellerService.RegisterResellerAsync(resellerDTO);
            return Ok(registeredReseller);

        }
        /// <summary>
        /// Retrieves a reseller by its API key.
        /// </summary>
        /// <param name="apiKey">The API key of the reseller to retrieve.</param>
        /// <returns>An IActionResult containing the reseller information if found, or NotFound if not found.</returns>
        [HttpGet("apikey/{apiKey}")]
        public async Task<IActionResult> GetResellerByApiKey(string apiKey)
        {
            var reseller = await _resellerService.GetResellerByApiKeyAsync(apiKey);
            if (reseller == null)
            {
                return NotFound();

            }
            var resellerDTO = _mapper.Map<ResellerDTO>(reseller);
            return Ok(new { Reseller = resellerDTO });

        }
    }
}
