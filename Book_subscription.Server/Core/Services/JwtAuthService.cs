using Book_subscription.Server.API.DTOs.Authentication;
using Book_subscription.Server.Core.Configurations;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Book_subscription.Server.Core.Services
{
    /// <summary>
    /// JWT authentication service implementation.
    /// </summary>
    public class JwtAuthService : IJwtAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<JwtAuthService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtAuthService"/> class.
        /// </summary>
        /// <param name="userManager">User manager for handling user-related operations.</param>
        /// <param name="jwtSettings">JWT settings configuration.</param>
        /// <param name="logger">Logger for logging information, warnings, and errors.</param>
        public JwtAuthService(UserManager<User> userManager, IOptions<JwtSettings> jwtSettings, ILogger<JwtAuthService> logger)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates a user with the provided login credentials.
        /// </summary>
        /// <param name="loginDTO">Login DTO containing user email and password.</param>
        /// <returns>User response DTO containing user details and JWT token.</returns>
        /// <summary>
        /// Authenticates a user with the provided login credentials.
        /// </summary>
        /// <param name="loginDTO">Login DTO containing user email and password.</param>
        /// <returns>User response DTO containing user details and JWT token.</returns>
        public async Task<UserResponseDTO> AuthenticateAsync(LoginUserDTO loginDTO)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (user == null || !await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                {
                    _logger.LogWarning("Authentication failed for email: {Email}", loginDTO.Email);
                    return null;
                }

                var token = GenerateJwtToken(user);
                return new UserResponseDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during authentication for email: {Email}", loginDTO.Email);
                throw new ApplicationException("An error occurred during authentication. Please try again later.", ex);
            }
        }
        /// <summary>
        /// Registers a new user with the provided registration details.
        /// </summary>
        /// <param name="registerDTO">Register DTO containing user details for registration.</param>
        /// <returns>User response DTO containing user details and JWT token.</returns>
        public async Task<UserResponseDTO> RegisterAsync(RegisterUserDTO registerDTO)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);
                if (existingUser != null)
                {
                    _logger.LogWarning("Registration failed. Email already exists: {Email}", registerDTO.Email);
                    return null;
                }

                var newUser = new User
                {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email
                };

                var result = await _userManager.CreateAsync(newUser, registerDTO.Password);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Registration failed for email: {Email}. Errors: {Errors}", registerDTO.Email, string.Join(", ", result.Errors));
                    return null;
                }

                var token = GenerateJwtToken(newUser);
                return new UserResponseDTO
                {
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during registration for email: {Email}", registerDTO.Email);
                throw new ApplicationException("An error occurred during registration. Please try again later.", ex);
            }
        }

        /// <summary>
        /// Generates a JWT token for the authenticated user.
        /// </summary>
        /// <param name="user">The authenticated user.</param>
        /// <returns>The generated JWT token.</returns>
        public string GenerateJwtToken(User user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireHours),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating JWT token for user: {UserName}", user.UserName);
                throw new ApplicationException("An error occurred while generating the token. Please try again later.", ex);
            }
        }
    }


}

