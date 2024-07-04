using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;

namespace Book_subscription.Server.API.DTOs.Book
{

    /// <summary>
    /// DTO for retrieving a book.
    /// </summary>
    public class BookDTO
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [Range(0.01, 1000.00)]
        public decimal PurchasedPrice { get; set; }
    }
}
