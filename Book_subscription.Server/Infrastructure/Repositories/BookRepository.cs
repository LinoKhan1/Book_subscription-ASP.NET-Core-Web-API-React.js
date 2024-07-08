using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book_subscription.Server.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing book data access.
    /// </summary>
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BookRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger.</param>
        public BookRepository(ApplicationDbContext context, ILogger<BookRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Asynchronously retrieves all books.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of books.</returns>
        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            try
            {
                return await _context.Books.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all books.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously retrieves a book by its identifier.
        /// </summary>
        /// <param name="bookId">The identifier of the book to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the book with the specified identifier.</returns>
        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            try
            {
                return await _context.Books.FindAsync(bookId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the book with ID {BookId}.", bookId);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously adds a new book.
        /// </summary>
        /// <param name="book">The book to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddBookAsync(Book book)
        {
            try
            {
                await _context.Books.AddAsync(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new book.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates an existing book.
        /// </summary>
        /// <param name="book">The book to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UpdateBookAsync(Book book)
        {
            try
            {
                _context.Entry(book).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book.");
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes a book by its identifier.
        /// </summary>
        /// <param name="bookId">The identifier of the book to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeleteBookAsync(int bookId)
        {
            try
            {
                var book = await _context.Books.FindAsync(bookId);
                if (book != null)
                {
                    _context.Books.Remove(book);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book with ID {BookId}.", bookId);
                throw;
            }
        }
    }
}
