using Book_subscription.Server.API.DTOs.Authentication;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Book_subscription.Server.API.Controllers
{
    /// <summary>
    /// Controller for handling user authentication (registration and login).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerUserDTO">DTO containing user registration details.</param>
        /// <returns>Returns IActionResult with user details and JWT token upon successful registration.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            try
            {
                var response = await _userService.RegisterAsync(registerUserDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration");
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="loginUserDTO">DTO containing user login credentials (email and password).</param>
        /// <returns>Returns IActionResult with user details and JWT token upon successful login.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            try
            {
                var response = await _userService.LoginAsync(loginUserDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login");
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
