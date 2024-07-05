using Book_subscription.Server.Core.Configurations;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Data;
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

        public ApiKeyService(IOptions<ApiKeySettings> apiKeySettings, ApplicationDbContext context)
        {
            _apiKeySettings = apiKeySettings.Value;
            _context = context;

        }

        public async Task<string> GenerateApiKeyAsync()
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
            await _context.SaveChangesAsync();  

            return apiKey;
        }

        private string HashApiKey(string apiKey)
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
        public async Task<bool> ValidateApiKeyAsync(string apiKey)
        {
            var hashedApiKey = HashApiKey(apiKey);
            return await _context.ApiKeys.AnyAsync(k => k.Key == hashedApiKey);
        }

    }
}
