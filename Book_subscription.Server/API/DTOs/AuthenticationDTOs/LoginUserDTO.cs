using System.ComponentModel.DataAnnotations;

namespace Book_subscription.Server.API.DTOs.Authentication
{
    /// <summary>
    /// Data Transfer Object (DTO) for user login.
    /// </summary>
    public class LoginUserDTO
    {

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
