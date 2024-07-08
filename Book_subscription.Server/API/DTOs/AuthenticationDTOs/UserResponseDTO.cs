namespace Book_subscription.Server.API.DTOs.Authentication
{
    /// <summary>
    /// Data Transfer Object (DTO) for user response after authentication.
    /// </summary>
    public class UserResponseDTO
    {
       
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
