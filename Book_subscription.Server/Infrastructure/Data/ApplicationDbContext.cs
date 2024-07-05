using Book_subscription.Server.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Book_subscription.Server.Infrastructure.Data
{
    /// <summary>
    /// Represents the database context for the Book Subscription application.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the database context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the DbSet of books in the database.
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the DbSet of resellers in the database
        /// </summary>
        public DbSet<Reseller> Resellers { get; set; }  

        /// <summary>
        /// Gets or sets the DbtSet of ApiKeys in the databse for resellers authentication
        /// </summary>
        public DbSet<ApiKey> ApiKeys { get; set; }
        
        /// <summary>
        /// Gets or sets the DbSet for Books.
        /// </summary>
        public DbSet<Subscription> Subscriptions { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Subscription>()
                .HasKey(x => x.SubsciptionId);

            // Configure relationships and constraints
            
        }
    }
}
