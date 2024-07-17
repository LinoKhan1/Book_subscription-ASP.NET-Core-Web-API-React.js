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

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userManager">User manager for handling user-related operations.</param>
        /// <param name="jwtAuthService">JWT authentication service.</param>
        public UserService(UserManager<User> userManager, IJwtAuthService jwtAuthService)
        {
            _userManager = userManager;
            _jwtAuthService = jwtAuthService;
        }

        public async Task<UserResponseDTO> RegisterAsync(RegisterUserDTO registerDTO)
        {
            return await _jwtAuthService.RegisterAsync(registerDTO);
        }

        public async Task<UserResponseDTO> LoginAsync(LoginUserDTO loginDTO)
        {
            return await _jwtAuthService.AuthenticateAsync(loginDTO);
        }
    }
}
