using System.ComponentModel.DataAnnotations;

namespace Book_subscription.Server.API.DTOs.SubscriptionDTOs
{
    /// <summary>
    /// DTO for subscribing and unsubscribing to a book.
    /// </summary>
    public class SubscriptionDTO
    {

        [Required(ErrorMessage = "BookId is required.")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; }
    }
}
