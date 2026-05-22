using System.Net;
using System.Text.Json;

namespace FinanceTracker.Api.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger) {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            } catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = ex switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                BadHttpRequestException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Forbidden,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var response = new
            {
                statusCode = context.Response.StatusCode,
                message = ex.Message,
                detailed = ex.InnerException?.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}