using LibraryManager.Application.DTOs.Response;
using System.Net;
using System.Text.Json;

namespace LibraryManager.WebApi.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // move to next middleware / controller
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred.");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse
                {
                    Status = false,
                    StatusCode = context.Response.StatusCode,
                    Message = "Something went wrong.",
                    Response=null
                };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
            }
        }
    }
}
