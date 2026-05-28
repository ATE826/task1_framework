using System.Diagnostics;

namespace task1_framework.Middleware;

public class PerformanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMiddleware> _logger;

    public PerformanceMiddleware(RequestDelegate next, ILogger<PerformanceMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        var requestId = context.Items["RequestId"];

        _logger.LogInformation(
            "Request {RequestId} executed in {Elapsed} ms",
            requestId,
            stopwatch.ElapsedMilliseconds);
    }
}