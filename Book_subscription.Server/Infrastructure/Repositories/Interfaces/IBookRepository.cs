using System.Collections.Generic;
using System.Threading.Tasks;
using Book_subscription.Server.Core.Entities;

namespace Book_subscription.Server.Infrastructure.Repositories.Interfaces
{
    /// <summary>
    /// Defines the interface for book repository.
    /// </summary>
    public interface IBookRepository
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
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddBookAsync(Book book);

        /// <summary>
        /// Asynchronously updates an existing book.
        /// </summary>
        /// <param name="book">The book to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateBookAsync(Book book);

        /// <summary>
        /// Asynchronously deletes a book by its identifier.
        /// </summary>
        /// <param name="bookId">The identifier of the book to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteBookAsync(int bookId);
    }
}
