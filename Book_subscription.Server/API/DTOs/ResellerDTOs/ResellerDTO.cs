using System.ComponentModel.DataAnnotations;

namespace Book_subscription.Server.API.DTOs.ResellerDTOs
{
    /// <summary>
    /// DTO for transferring reseller information.
    /// </summary>
    public class ResellerDTO
    {
        
        public int ResellerId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ApiKey is required.")]
        public string ApiKey { get; set; }
    }
}
