using Book_subscription.Server.API.DTOs;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDTO> RegisterAsync (RegisterUserDTO registerUserDTO);
        Task<UserResponseDTO> LoginAsync (LoginUserDTO loginUserDTO);
    }
}
