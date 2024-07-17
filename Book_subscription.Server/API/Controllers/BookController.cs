using Book_subscription.Server.API.DTOs.Book;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Book_subscription.Server.API.Controllers
{
    /// <summary>
    /// API controller for managing books.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookController"/> class.
        /// </summary>
        /// <param name="bookService">The book service.</param>
        /// <param name="logger">The logger.</param>
        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }
        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <returns>A list of books.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _bookService.GetBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all books.");
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Retrieves a book by its identifier.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <returns>The book with the specified identifier.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the book with ID {BookId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Adds a new book.
        /// </summary>
        /// <param name="bookDTO">The book data transfer object.</param>
        /// <returns>The added book.</returns>
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook([FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var book = new Book
                {
                    Name = bookDTO.Name,
                    Text = bookDTO.Text,
                    PurchasePrice = bookDTO.PurchasedPrice
                };

                var addedBook = await _bookService.AddBookAsync(book);
                return CreatedAtAction(nameof(GetBook), new { id = addedBook.BookId }, addedBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new book.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <param name="bookDTO">The book data transfer object.</param>
        /// <returns>No content if update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var book = new Book
                {
                    BookId = id,
                    Name = bookDTO.Name,
                    Text = bookDTO.Text,
                    PurchasePrice = bookDTO.PurchasedPrice
                };

                await _bookService.UpdateBookAsync(id, book);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Book not found: {BookId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book with ID {BookId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Deletes a book by its identifier.
        /// </summary>
        /// <param name="id">The book identifier.</param>
        /// <returns>No content if deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Book not found: {BookId}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book with ID {BookId}.", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
