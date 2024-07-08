using System.Threading.Tasks;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    /// <summary>
    /// Interface for JWT authentication service.
    /// </summary>
    public interface IJwtAuthService
    {
        /// <summary>
        /// Generates a JWT token for the specified user ID and username.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="userName">The username of the user.</param>
        /// <returns>The generated JWT token.</returns>
        Task<string> GenerateJwtTokenAsync(string userId, string userName);

        /// <summary>
        /// Verifies the validity of a JWT token.
        /// </summary>
        /// <param name="token">The JWT token to verify.</param>
        /// <returns>True if the token is valid; otherwise, false.</returns>
        Task<bool> VerifyJwtTokenAsync(string token);
    }
}
