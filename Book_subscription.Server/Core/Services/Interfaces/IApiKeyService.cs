namespace Book_subscription.Server.Core.Services.Interfaces
{
    public interface IApiKeyService
    {
        Task<string> GenerateApiKeyAsync();

        Task<bool> ValidateApiKeyAsync(string apiKey);

        string HashApiKey(string apiKey);   
    }
}
