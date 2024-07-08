using System.ComponentModel.DataAnnotations;

namespace Book_subscription.Server.API.DTOs.Book
{
    /// <summary>
    /// DTO for retrieving a book.
    /// </summary>
    public class BookDTO
    {
        
        public int BookId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [MaxLength(500, ErrorMessage = "Text cannot exceed 500 characters.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Purchase Price is required.")]
        [Range(0.01, 1000.00, ErrorMessage = "Purchase Price must be between 0.01 and 1000.00.")]
        public decimal PurchasedPrice { get; set; }
    }
}
