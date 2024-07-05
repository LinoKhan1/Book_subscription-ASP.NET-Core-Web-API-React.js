using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_subscription.tests.Repositories
{

    /// <summary>
    /// Unit tests for the BookRepository class.
    /// </summary>
    public class BookRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly BookRepository _repository;


        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepositoryTests"/> class.
        /// Sets up the in-memory database and seeds initial data.
        /// </summary>
        public BookRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BookDatabase")
                .Options;
            _context = new ApplicationDbContext(_options);
            _repository = new BookRepository(_context);

            SeedDatabase();
        }
        /// <summary>
        /// Seeds the in-memory database with initial data.
        /// </summary>
        private void SeedDatabase()
        {
            var books = new List<Book>
            {
                new Book { BookId = 1, Name = "Book 1", Text = "Text 1", PurchasePrice = 9.99m },
                new Book { BookId = 2, Name = "Book 2", Text = "Text 2", PurchasePrice = 19.99m }
            };

            _context.Books.AddRange(books);
            _context.SaveChanges();
        }
        /// <summary>
        /// Tests that GetBooksAsync returns all books in the database.
        /// </summary>
        [Fact]
        public async Task GetBooksAsync_ReturnsAllBooks()
        {
            var result = await _repository.GetBooksAsync();

            Assert.Equal(2, result.Count());
        }


        /// <summary>
        /// Tests that GetBookByIdAsync returns the correct book for a given ID.
        /// </summary>
        [Fact]
        public async Task GetBookByIdAsync_ReturnsCorrectBook()
        {
            var result = await _repository.GetBookByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.BookId);
            Assert.Equal("Book 1", result.Name);
        }

        /// <summary>
        /// Tests that GetBookByIdAsync returns null if the book is not found.
        /// </summary>
        [Fact]
        public async Task GetBookByIdAsync_ReturnsNullIfNotFound()
        {
            var result = await _repository.GetBookByIdAsync(99);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests that AddBookAsync adds a new book to the database.
        /// </summary>
        [Fact]
        public async Task AddBookAsync_AddsNewBook()
        {
            var newBook = new Book { BookId = 3, Name = "Book 3", Text = "Text 3", PurchasePrice = 29.99m };

            await _repository.AddBookAsync(newBook);
            await _context.SaveChangesAsync();

            var result = await _repository.GetBookByIdAsync(3);

            Assert.NotNull(result);
            Assert.Equal(3, result.BookId);
        }

        /// <summary>
        /// Tests that UpdateBookAsync updates the details of an existing book.
        /// </summary>
        [Fact]
        public async Task UpdateBookAsync_UpdatesExistingBook()
        {
            var book = await _repository.GetBookByIdAsync(1);
            book.Name = "Updated Book 1";

            await _repository.UpdateBookAsync(book);
            await _context.SaveChangesAsync();

            var updatedBook = await _repository.GetBookByIdAsync(1);

            Assert.Equal("Updated Book 1", updatedBook.Name);
        }

        /// <summary>
        /// Tests that DeleteBookAsync deletes the correct book for a given ID.
        /// </summary>
        [Fact]
        public async Task DeleteBookAsync_DeletesCorrectBook()
        {
            await _repository.DeleteBookAsync(1);
            await _context.SaveChangesAsync();

            var result = await _repository.GetBookByIdAsync(1);

            Assert.Null(result);
        }

        /// <summary>
        /// Tests that DeleteBookAsync does nothing if the book is not found.
        /// </summary>
        [Fact]
        public async Task DeleteBookAsync_DoesNothingIfNotFound()
        {
            var initialCount = _context.Books.Count();

            await _repository.DeleteBookAsync(99);
            await _context.SaveChangesAsync();

            var finalCount = _context.Books.Count();

            Assert.Equal(initialCount, finalCount);
        }

    }
}
