using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;

namespace Book_subscription.Server.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepository;

        public BookService(IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _bookRepository.GetBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _bookRepository.GetBookByIdAsync(bookId);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _bookRepository.AddBookAsync(book);
            await _unitOfWork.CompleteAsync(); // Persist changes
            return book;
        }

        public async Task<Book> UpdateBookAsync(int bookId, Book book)
        {
            var existingBook = await _bookRepository.GetBookByIdAsync(bookId);
            if (existingBook == null)
            {
                throw new ArgumentException("Book not found");
            }

            existingBook.Name = book.Name;
            existingBook.Text = book.Text;
            existingBook.PurchasePrice = book.PurchasePrice;

            await _bookRepository.UpdateBookAsync(existingBook);
            await _unitOfWork.CompleteAsync(); // Persist changes

            return existingBook;
        }

        public async Task DeleteBookAsync(int bookId)
        {
            await _bookRepository.DeleteBookAsync(bookId);
            await _unitOfWork.CompleteAsync(); // Persist changes
        }
    }
}
