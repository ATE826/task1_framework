namespace task1_framework.Middleware;

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
        var requestId = Guid.NewGuid().ToString();
        context.Items["RequestId"] = requestId;

        _logger.LogInformation("Request {RequestId}: {Method} {Path}",
            requestId,
            context.Request.Method,
            context.Request.Path);

        await _next(context);

        _logger.LogInformation("Response {RequestId}: {StatusCode}",
            requestId,
            context.Response.StatusCode);
    }
}