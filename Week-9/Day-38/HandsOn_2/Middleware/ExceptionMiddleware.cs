using System.Text.Json;
using WebApplication14.Exceptions;
using WebApplication14.Models;

namespace WebApplication14.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.Clear();
            var response = new ErrorResponse
            {
                Timestamp = DateTime.UtcNow
            };

            if (ex is NotFoundException)
            {
                response.StatusCode = 404;
                response.Message = ex.Message;
                context.Response.StatusCode = 404;
                _logger.LogWarning(ex, "Not Found Exception");
            }
            else if (ex is BadRequestException)
            {
                response.StatusCode = 400;
                response.Message = ex.Message;
                context.Response.StatusCode = 400;
                _logger.LogWarning(ex, "Bad Request Exception");
            }
            else
            {
                response.StatusCode = 500;
                response.Message = "Something went wrong";
                context.Response.StatusCode = 500;
                _logger.LogError(ex, "Unhandled Exception");
            }

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
