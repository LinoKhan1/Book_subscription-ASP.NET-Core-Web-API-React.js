using Book_subscription.Server.API.DTOs.Authentication;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Book_subscription.Server.Core.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<User> _userManager;
        private readonly IJwtAuthService _jwtAuthService;

        public UserService(UserManager<User> userManager, IJwtAuthService jwtAuthService)
        {
            _userManager = userManager;
            _jwtAuthService = jwtAuthService;
        }

        public async Task<UserResponseDTO> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            var user = new User
            {
                UserName = registerUserDTO.UserName,
                Email = registerUserDTO.Email,
            };
            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            }
            var token = _jwtAuthService.GenerateJwtTokenAsync(user.Id, user.UserName);

            return new UserResponseDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await token

            };
        }
        public async Task<UserResponseDTO> LoginAsync(LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDTO.Password))
            {
                throw new Exception("Invalid login attempt.");
            }

            var token = _jwtAuthService.GenerateJwtTokenAsync(user.Id, user.UserName);
            return new UserResponseDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await token

            };

        }
    }
}
