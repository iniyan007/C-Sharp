using System.Net;
using System.Text.Json;

namespace BooksApi.Middleware;

// ─────────────────────────────────────────────────────────────────────────────
// MIDDLEWARE
//
// Middleware is code that runs in the HTTP request/response PIPELINE.
// Every request passes through middleware in order, like a chain.
//
// Request → [Middleware 1] → [Middleware 2] → [Controller] → [Middleware 2] → [Middleware 1] → Response
//
// This middleware catches ALL unhandled exceptions and returns a clean JSON error
// instead of a crash page. Without this, clients would see HTML error pages.
// ─────────────────────────────────────────────────────────────────────────────

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;     // The next middleware in the pipeline
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next middleware/controller in the pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            // If anything throws, we catch it here before it reaches the client
            _logger.LogError(ex, "Unhandled exception occurred for {Method} {Path}",
                context.Request.Method, context.Request.Path);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Map exception types to HTTP status codes
        var (statusCode, message) = exception switch
        {
            InvalidOperationException => (HttpStatusCode.Conflict, exception.Message),
            ArgumentException         => (HttpStatusCode.BadRequest, exception.Message),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Access denied"),
            _                         => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
        };

        context.Response.StatusCode = (int)statusCode;

        // Return a consistent error shape — clients always know what to expect
        var errorResponse = new
        {
            StatusCode = (int)statusCode,
            Message = message,
            Timestamp = DateTime.UtcNow,
            TraceId = context.TraceIdentifier // Useful for log correlation
        };

        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}

// Extension method — makes registration in Program.cs read naturally:
// app.UseGlobalExceptionMiddleware()
public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}