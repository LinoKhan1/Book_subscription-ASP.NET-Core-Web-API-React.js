using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;

namespace Book_subscription.Server.Core.Services
{
    /// <summary>
    /// Provides services for managing books.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="bookRepository">The book repository.</param>
        /// <param name="logger">The logger.</param>
        public BookService(IUnitOfWork unitOfWork, IBookRepository bookRepository, ILogger<BookService> logger)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
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
                return await _bookRepository.GetBooksAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving books.");
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
                return await _bookRepository.GetBookByIdAsync(bookId);
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
        /// <returns>A task that represents the asynchronous operation. The task result contains the added book.</returns>
        public async Task<Book> AddBookAsync(Book book)
        {
            try
            {
                await _bookRepository.AddBookAsync(book);
                await _unitOfWork.CompleteAsync(); // Persist changes
                return book;
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
        /// <param name="bookId">The identifier of the book to update.</param>
        /// <param name="book">The updated book information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated book.</returns>
        public async Task<Book> UpdateBookAsync(int bookId, Book book)
        {
            try
            {
                var existingBook = await _bookRepository.GetBookByIdAsync(bookId);
                if (existingBook == null)
                {
                    _logger.LogWarning("Book with ID {BookId} not found.", bookId);
                    throw new ArgumentException("Book not found");
                }

                existingBook.Name = book.Name;
                existingBook.Text = book.Text;
                existingBook.PurchasePrice = book.PurchasePrice;

                await _bookRepository.UpdateBookAsync(existingBook);
                await _unitOfWork.CompleteAsync(); // Persist changes

                return existingBook;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book with ID {BookId}.", bookId);
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
                await _bookRepository.DeleteBookAsync(bookId);
                await _unitOfWork.CompleteAsync(); // Persist changes
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book with ID {BookId}.", bookId);
                throw;
            }
        }
    }
}
