using Book_subscription.Server.API.DTOs.Authentication;
using Book_subscription.Server.Core.Entities;
using System.Threading.Tasks;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    /// <summary>
    /// Interface for JWT authentication service.
    /// </summary>
    public interface IJwtAuthService
    {
        Task<UserResponseDTO> AuthenticateAsync(LoginUserDTO loginDTO);
        Task<UserResponseDTO> RegisterAsync(RegisterUserDTO registerDTO);

        string GenerateJwtToken(User user);


    }
}
