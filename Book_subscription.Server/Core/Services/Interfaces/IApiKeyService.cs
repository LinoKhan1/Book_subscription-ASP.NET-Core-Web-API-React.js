namespace Book_subscription.Server.Core.Services.Interfaces
{
    /// <summary>
    /// Interface for managing API keys.
    /// </summary>
    public interface IApiKeyService
    {
        /// <summary>
        /// Generates a new API key asynchronously.
        /// </summary>
        /// <returns>The generated API key as a string.</returns>
        Task<string> GenerateApiKeyAsync();

        /// <summary>
        /// Validates if the provided API key is valid asynchronously.
        /// </summary>
        /// <param name="apiKey">The API key to validate.</param>
        /// <returns>True if the API key is valid; otherwise, false.</returns>
        Task<bool> ValidateApiKeyAsync(string apiKey);

        /// <summary>
        /// Hashes the provided API key string.
        /// </summary>
        /// <param name="apiKey">The API key to hash.</param>
        /// <returns>The hashed API key.</returns>
        string HashApiKey(string apiKey);
    }
}
