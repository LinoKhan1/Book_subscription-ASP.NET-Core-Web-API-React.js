using Book_subscription.Server.Core.Configurations;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Data;
using Book_subscription.Server.Infrastructure.unitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Book_subscription.Server.Core.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly ApiKeySettings _apiKeySettings;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApiKeyService> _logger;

        public ApiKeyService(IOptions<ApiKeySettings> apiKeySettings, ApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<ApiKeyService> logger)
        {
            _apiKeySettings = apiKeySettings.Value;
            _context = context;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Generates a new API key asynchronously.
        /// </summary>
        /// <returns>The generated API key as a string.</returns>
        public async Task<string> GenerateApiKeyAsync()
        {
            try
            {
                using var hmac = new HMACSHA256();
                var apiKey = Convert.ToBase64String(hmac.Key);

                var hashedApiKey = HashApiKey(apiKey);
                var apiKeyEntity = new ApiKey
                {
                    Key = hashedApiKey,
                    CreatedAt = DateTime.UtcNow,
                };

                _context.ApiKeys.Add(apiKeyEntity);
                await _unitOfWork.CompleteAsync();

                return apiKey;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error generating API key: {ex.Message}");
                throw new Exception("Error generating API key", ex);
            }
        }


        /// <summary>
        /// Hashes the provided API key string.
        /// </summary>
        /// <param name="apiKey">The API key to hash.</param>
        /// <returns>The hashed API key.</returns>

        public string HashApiKey(string apiKey)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(apiKey));
            return Convert.ToBase64String(hash);
        }
        private string EncryptApiKey(string apiKey)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_apiKeySettings.EncryptionKey);
            aes.IV = new byte[16]; // Initialization vector

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(apiKey);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        private string DecryptApiKey(string encryptedApiKey)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(_apiKeySettings.EncryptionKey);
            aes.IV = new byte[16];

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(Convert.FromBase64String(encryptedApiKey));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);

            return sr.ReadToEnd();
        }
        /// <summary>
        /// Validates if the provided API key is valid asynchronously.
        /// </summary>
        /// <param name="apiKey">The API key to validate.</param>
        /// <returns>True if the API key is valid; otherwise, false.</returns>
        public async Task<bool> ValidateApiKeyAsync(string apiKey)
        {
            try
            {
                var hashedApiKey = HashApiKey(apiKey);
                return await _context.ApiKeys.AnyAsync(k => k.Key == hashedApiKey);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error validating API key: {ex.Message}");
                throw new Exception("Error validating API key", ex);
            }
        }
    }
}
