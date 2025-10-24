using System.Net;
using System.Text.Json;

namespace WebAPI.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, _logger);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
    {
        var errorId = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        var errorCode = "UnexpectedError"; // Stable error code for client-side localization

        logger.LogError(exception, "An unexpected error occurred. Error ID: {ErrorId}, Error Code: {ErrorCode}", errorId, errorCode);

        var result = JsonSerializer.Serialize(new
        {
            ErrorId = errorId,
            ErrorCode = errorCode,
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "An unexpected error occurred. Please use the errorId to track this issue." // Fallback message
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(result);
    }
}
