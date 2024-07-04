using System.Security.Claims;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    public interface IJwtAuthService
    {
        Task<string> GenerateJwtTokenAsync(string userId, string UserName);
        Task<bool> VerifyJwtTokenAsync(string token);
    }
}
