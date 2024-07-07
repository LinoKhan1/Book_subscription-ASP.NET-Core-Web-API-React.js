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
    [Route("api/[controller]")]
    [ApiController]
    public class ResellerController : ControllerBase
    {
        private readonly IResellerService _resellerService;
        private readonly IMapper _mapper;

        public ResellerController(IResellerService resellerService, IMapper mapper)
        {
            _resellerService = resellerService;
            _mapper = mapper;
        }

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
