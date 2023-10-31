using System.Diagnostics;

namespace CinemaTicketBooking.Api.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await _next(context);
        stopwatch.Stop();

        var elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
        var statusCode = context.Response.StatusCode;

        _logger.LogInformation("Request {Method} {Url} completed in {ElapsedTime} ms with status code {StatusCode}",
            context.Request.Method,
            context.Request.Path,
            elapsedTime,
            statusCode);
    }
}