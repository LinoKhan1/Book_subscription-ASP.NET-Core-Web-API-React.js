/*using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services;
using Book_subscription.Server.Infrastructure.Repositories.Interfaces;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Book_subscription.tests.Services.Unit
{
    /// <summary>
    /// Unit tests for BookService class.
    /// </summary>
    public class BookServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookService = new BookService(_unitOfWorkMock.Object, _bookRepositoryMock.Object);
        }

        [Fact]
        public async Task GetBooksAsync_ShouldReturnListOfBooks()
        {
            // Arrange
            var books = new List<Book> { new Book { BookId = 1, Name = "Test Book" } };
            _bookRepositoryMock.Setup(repo => repo.GetBooksAsync()).ReturnsAsync(books);

            // Act
            var result = await _bookService.GetBooksAsync();

            // Assert
            Assert.Equal(books, result);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var book = new Book { BookId = 1, Name = "Test Book" };
            _bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _bookService.GetBookByIdAsync(1);

            // Assert
            Assert.Equal(book, result);
        }

        [Fact]
        public async Task GetBookByIdAsync_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            _bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            var result = await _bookService.GetBookByIdAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddBookAsync_ShouldAddBookAndCallCompleteAsync()
        {
            // Arrange
            var book = new Book { BookId = 1, Name = "Test Book" };

            // Act
            var result = await _bookService.AddBookAsync(book);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.AddBookAsync(book), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
            Assert.Equal(book, result);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldUpdateBookAndCallCompleteAsync_WhenBookExists()
        {
            // Arrange
            var bookId = 1;
            var existingBook = new Book { BookId = bookId, Name = "Existing Book" };
            var updatedBook = new Book { BookId = bookId, Name = "Updated Book", Text = "Updated Text", PurchasePrice = 20.00m };

            _bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(bookId)).ReturnsAsync(existingBook);

            // Act
            var result = await _bookService.UpdateBookAsync(bookId, updatedBook);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.UpdateBookAsync(existingBook), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
            Assert.Equal(updatedBook.Name, result.Name);
            Assert.Equal(updatedBook.Text, result.Text);
            Assert.Equal(updatedBook.PurchasePrice, result.PurchasePrice);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldThrowException_WhenBookDoesNotExist()
        {
            // Arrange
            var bookId = 1;
            var updatedBook = new Book { BookId = bookId, Name = "Updated Book", Text = "Updated Text", PurchasePrice = 20.00m };

            _bookRepositoryMock.Setup(repo => repo.GetBookByIdAsync(bookId)).ReturnsAsync((Book)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _bookService.UpdateBookAsync(bookId, updatedBook));
        }

        [Fact]
        public async Task DeleteBookAsync_ShouldDeleteBookAndCallCompleteAsync()
        {
            // Arrange
            var bookId = 1;

            // Act
            await _bookService.DeleteBookAsync(bookId);

            // Assert
            _bookRepositoryMock.Verify(repo => repo.DeleteBookAsync(bookId), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
        }
    }
}
*/