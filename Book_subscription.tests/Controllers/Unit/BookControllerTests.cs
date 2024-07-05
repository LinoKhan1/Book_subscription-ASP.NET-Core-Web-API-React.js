using Book_subscription.Server.API.Controllers;
using Book_subscription.Server.API.DTOs.Book;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_subscription.tests.Controllers.Unit
{
    public class BookControllerTests
    {
        private readonly Mock<IBookService> _mockBookService;
        private readonly BookController _bookController;

        public BookControllerTests()
        {
            _mockBookService = new Mock<IBookService>();
            _bookController = new BookController(_mockBookService.Object);
        }

        [Fact]
        public async Task GetBooks_ReturnsOkResult_WithListOfBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { BookId = 1, Name = "Book 1", Text = "Text 1", PurchasePrice = 10.0m },
                new Book { BookId = 2, Name = "Book 2", Text = "Text 2", PurchasePrice = 20.0m }
            };
            _mockBookService.Setup(service => service.GetBooksAsync()).ReturnsAsync(books);

            // Act
            var result = await _bookController.GetBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Book>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetBook_ReturnsOkResult_WithBook()
        {
            // Arrange
            var book = new Book { BookId = 1, Name = "Book 1", Text = "Text 1", PurchasePrice = 10.0m };
            _mockBookService.Setup(service => service.GetBookByIdAsync(1)).ReturnsAsync(book);

            // Act
            var result = await _bookController.GetBook(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(1, returnValue.BookId);
        }

        [Fact]
        public async Task GetBook_ReturnsNotFoundResult_WhenBookDoesNotExist()
        {
            // Arrange
            _mockBookService.Setup(service => service.GetBookByIdAsync(1)).ReturnsAsync((Book)null);

            // Act
            var result = await _bookController.GetBook(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task AddBook_ReturnsCreatedAtActionResult_WithBook()
        {
            // Arrange
            var bookDTO = new BookDTO { Name = "Book 1", Text = "Text 1", PurchasedPrice = 10.0m };
            var book = new Book { BookId = 1, Name = "Book 1", Text = "Text 1", PurchasePrice = 10.0m };
            _mockBookService.Setup(service => service.AddBookAsync(It.IsAny<Book>())).ReturnsAsync(book);

            // Act
            var result = await _bookController.AddBook(bookDTO);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<Book>(createdAtActionResult.Value);
            Assert.Equal(1, returnValue.BookId);
        }

        [Fact]
        public async Task UpdateBook_ReturnsNoContentResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var bookDTO = new BookDTO { Name = "Updated Book", Text = "Updated Text", PurchasedPrice = 15.0m };
            _mockBookService.Setup(service => service.UpdateBookAsync(1, It.IsAny<Book>())).ReturnsAsync(new Book());

            // Act
            var result = await _bookController.UpdateBook(1, bookDTO);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateBook_ReturnsNotFoundResult_WhenBookDoesNotExist()
        {
            // Arrange
            var bookDTO = new BookDTO { Name = "Updated Book", Text = "Updated Text", PurchasedPrice = 15.0m };
            _mockBookService.Setup(service => service.UpdateBookAsync(1, It.IsAny<Book>())).ThrowsAsync(new ArgumentException("Book not found"));

            // Act
            var result = await _bookController.UpdateBook(1, bookDTO);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Book not found", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNoContentResult_WhenDeleteIsSuccessful()
        {
            // Arrange
            _mockBookService.Setup(service => service.DeleteBookAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _bookController.DeleteBook(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNotFoundResult_WhenBookDoesNotExist()
        {
            // Arrange
            _mockBookService.Setup(service => service.DeleteBookAsync(1)).ThrowsAsync(new ArgumentException("Book not found"));

            // Act
            var result = await _bookController.DeleteBook(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Book not found", notFoundResult.Value);
        }
    }
}
