using System.Text.Json;
using task1_framework.Models;

namespace task1_framework.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                KeyNotFoundException => 404,
                ArgumentException => 400,
                _ => 500
            };

            context.Response.StatusCode = statusCode;

            var requestId = context.Items["RequestId"]?.ToString() ?? "";

            var error = new ErrorResponse
            {
                ErrorCode = statusCode.ToString(),
                Message = ex.Message,
                RequestId = requestId
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}