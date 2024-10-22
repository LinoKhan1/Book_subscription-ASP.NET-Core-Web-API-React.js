﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Book_subscription.Server.Core.Entities
{
    /// <summary>
    /// Represents a subscription of a user to a book.
    /// </summary>
    public class Subscription
    {
        
        public int SubscriptionId { get; set; }

        public int BookId { get; set; }

        public string UserId { get; set; }

        public DateTime SubscriptionDate { get; set; }

        public User User { get; set; }

        public Book Book { get; set; }
    }
}
