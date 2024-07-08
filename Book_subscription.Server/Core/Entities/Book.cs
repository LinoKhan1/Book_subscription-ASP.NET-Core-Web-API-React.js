using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book_subscription.Server.Core.Entities
{
    /// <summary>
    /// Represents a book in the subscription system.
    /// </summary>
    public class Book
    {
        
        public int BookId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public decimal PurchasePrice { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
