using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_subscription.Server.Core.Entities
{
    /// <summary>
    /// Represents a book in the subscription system.
    /// </summary>
    public class Book
    {
       
        public int BookId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Text { get; set; }
        public decimal PurchasePrice { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }    
    }
}
