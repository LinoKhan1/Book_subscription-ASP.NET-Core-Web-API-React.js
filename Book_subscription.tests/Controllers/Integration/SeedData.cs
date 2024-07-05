using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_subscription.tests.Controllers.Integration
{
    public static class SeedData
    {
        public static void PopulateTestData(ApplicationDbContext context)
        {
            // Add sample books
            context.Books.AddRange(new[]
            {
                new Book { BookId = 1, Name = "Book 1", Text = "Sample text for Book 1", PurchasePrice = 10.99m },
                new Book { BookId = 2, Name = "Book 2", Text = "Sample text for Book 2", PurchasePrice = 15.49m }
            });

            context.SaveChanges();
        }
    }
}
