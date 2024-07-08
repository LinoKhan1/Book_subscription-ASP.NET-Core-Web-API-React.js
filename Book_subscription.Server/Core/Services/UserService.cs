using Book_subscription.Server.API.DTOs.Authentication;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Book_subscription.Server.Core.Services
{
    /// <summary>
    /// Service class for user authentication and registration operations.
    /// </summary>
    public class UserService : IUserService
    {

        
        private readonly UserManager<User> _userManager;
        private readonly IJwtAuthService _jwtAuthService;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager, IJwtAuthService jwtAuthService, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _jwtAuthService = jwtAuthService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="registerUserDTO">DTO containing user registration details.</param>
        /// <returns>Returns a response DTO containing user details and JWT token upon successful registration.</returns>
        public async Task<UserResponseDTO> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            try
            {
                var user = new User
                {
                    UserName = registerUserDTO.UserName,
                    Email = registerUserDTO.Email,
                };

                var result = await _userManager.CreateAsync(user, registerUserDTO.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new ApplicationException($"Failed to register user: {errors}");
                }

                var token = await _jwtAuthService.GenerateJwtTokenAsync(user.Id, user.UserName);

                return new UserResponseDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration");
                throw;
            }
        }
        /// </summary>
        /// <param name="loginUserDTO">DTO containing user login credentials (email and password).</param>
        /// <returns>Returns a response DTO containing user details and JWT token upon successful login.</returns>
        public async Task<UserResponseDTO> LoginAsync(LoginUserDTO loginUserDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);
                if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDTO.Password))
                {
                    throw new ApplicationException("Invalid login attempt.");
                }

                var token = await _jwtAuthService.GenerateJwtTokenAsync(user.Id, user.UserName);

                return new UserResponseDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user login");
                throw;
            }
        }
    }
}
