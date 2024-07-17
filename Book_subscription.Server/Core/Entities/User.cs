using Microsoft.AspNetCore.Identity;

namespace Book_subscription.Server.Core.Entities
{
    /// <summary>
    /// Represents a user in the book subscription system.
    /// </summary>
    public class User : IdentityUser
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            Subscriptions = new List<Subscription>();
        }
        /// <summary>
        /// Gets or sets the collection of subscriptions for the user.
        /// </summary>
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
