namespace Book_subscription.Server.Core.Configurations
{
    /// <summary>
    /// Represents JWT settings used for token generation and validation.
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Gets or sets the key used to sign JWT tokens.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the issuer of the JWT token.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the audience of the JWT token.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the expiration time of the JWT token in hours.
        /// </summary>
        public int ExpireHours { get; set; }
    }
}
