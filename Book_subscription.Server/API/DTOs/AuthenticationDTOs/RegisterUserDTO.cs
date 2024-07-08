using System.ComponentModel.DataAnnotations;

namespace Book_subscription.Server.API.DTOs.Authentication
{
    /// <summary>
    /// Data Transfer Object (DTO) for user registration.
    /// </summary>
    public class RegisterUserDTO
    {

        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
