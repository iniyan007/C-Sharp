using System.Diagnostics;

namespace BooksApi.Middleware;

/// <summary>
/// Logs every incoming request and its response — great for debugging and monitoring.
/// Shows: method, path, status code, and how long it took.
/// </summary>
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Start a timer BEFORE the request is processed
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation("→ {Method} {Path} started",
            context.Request.Method,
            context.Request.Path);

        // Let the request flow through the rest of the pipeline
        await _next(context);

        // AFTER the response is ready
        stopwatch.Stop();

        _logger.LogInformation("← {Method} {Path} → {StatusCode} in {ElapsedMs}ms",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestLoggingMiddleware>();
    }
}