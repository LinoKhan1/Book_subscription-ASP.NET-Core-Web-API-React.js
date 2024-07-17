using System.ComponentModel.DataAnnotations;

namespace Book_subscription.Server.API.DTOs.SubscriptionDTOs
{
    /// <summary>
    /// DTO for subscribing and unsubscribing to a book.
    /// </summary>
    public class SubscriptionDTO
    {

        public int SubscriptionId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}
