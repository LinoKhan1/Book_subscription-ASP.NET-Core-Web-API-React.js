namespace Book_subscription.Server.API.DTOs.SubscriptionDTOs
{
    /// <summary>
    /// DTO for subscribing and unsubscribing to a book.
    /// </summary>
    public class SubscriptionDTO
    {
        public int BookId { get; set; }
        public string UserId { get; set; }
    }
}
