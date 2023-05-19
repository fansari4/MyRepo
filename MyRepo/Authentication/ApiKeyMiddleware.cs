using Microsoft.Extensions.Configuration;

namespace MyRepo.Authentication
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "ApiKey";
        private string APIKEYVALUE;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _configuration = configuration; 
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            APIKEYVALUE = _configuration["Key"];
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided. (Using ApiKeyMiddleware) ");
                return;
            }

            if (!APIKEYVALUE.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client. (Using ApiKeyMiddleware)");
                return;
            }

            await _next(context);
        }
    }
}
