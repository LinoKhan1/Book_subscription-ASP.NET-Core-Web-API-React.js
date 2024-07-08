using System.Collections.Generic;
using System.Threading.Tasks;
using Book_subscription.Server.Core.Entities;

namespace Book_subscription.Server.Core.Services.Interfaces
{
    /// <summary>
    /// Defines the interface for book services.
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Asynchronously retrieves all books.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of books.</returns>
        Task<IEnumerable<Book>> GetBooksAsync();

        /// <summary>
        /// Asynchronously retrieves a book by its identifier.
        /// </summary>
        /// <param name="bookId">The identifier of the book to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the book with the specified identifier.</returns>
        Task<Book> GetBookByIdAsync(int bookId);

        /// <summary>
        /// Asynchronously adds a new book.
        /// </summary>
        /// <param name="book">The book to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added book.</returns>
        Task<Book> AddBookAsync(Book book);

        /// <summary>
        /// Asynchronously updates an existing book.
        /// </summary>
        /// <param name="bookId">The identifier of the book to update.</param>
        /// <param name="book">The updated book information.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated book.</returns>
        Task<Book> UpdateBookAsync(int bookId, Book book);

        /// <summary>
        /// Asynchronously deletes a book by its identifier.
        /// </summary>
        /// <param name="bookId">The identifier of the book to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteBookAsync(int bookId);
    }
}
