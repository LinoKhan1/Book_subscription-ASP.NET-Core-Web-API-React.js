using Book_subscription.Server.Core.Services.Interfaces;

namespace Book_subscription.Server.Core.Services
{
    public class ApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IResellerService _resellerService;
        private readonly ILogger<ApiKeyAuthenticationMiddleware> _logger;
        private readonly string _headerKey;

        public ApiKeyAuthenticationMiddleware(RequestDelegate next, IResellerService resellerService, ILogger<ApiKeyAuthenticationMiddleware> logger, string headerKey = "ApiKey")
        {
            _next = next;
            _resellerService = resellerService;
            _logger = logger;
            _headerKey = headerKey;
        }   

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(_headerKey, out var apiKey))
            {
                _logger.LogWarning("API key is missing");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API key is missing");
                return;
            }

            var reseller = await _resellerService.GetResellerByApiKeyAsync(apiKey);
            if (reseller == null)
            {
                _logger.LogWarning("Invalid API key");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            // Proceed to the next middleware if authentication is successful
            await _next(context);


        }
    }
}
