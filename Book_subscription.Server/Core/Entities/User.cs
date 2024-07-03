using Microsoft.AspNetCore.Identity;

namespace Book_subscription.Server.Core.Entities
{
    /// <summary>
    /// Represents a user in the book subscription system.
    /// </summary>
    public class User : IdentityUser
    {

       
        /// <summary>
        /// Gets or sets the collection of subscriptions for the user.
        /// </summary>
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
