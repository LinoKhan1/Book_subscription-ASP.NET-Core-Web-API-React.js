using Book_subscription.Server.API.DTOs.Authentication;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    /// <summary>
    /// Interface for user authentication and registration operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="registerUserDTO">DTO containing user registration details.</param>
        /// <returns>Returns a response DTO containing user details and JWT token upon successful registration.</returns>
        Task<UserResponseDTO> RegisterAsync(RegisterUserDTO registerUserDTO );

        /// <summary>
        /// Logs in a user asynchronously.
        /// </summary>
        /// <param name="loginUserDTO">DTO containing user login credentials (email and password).</param>
        /// <returns>Returns a response DTO containing user details and JWT token upon successful login.</returns>
        Task<UserResponseDTO> LoginAsync(LoginUserDTO loginUserDTO);
    }
}
