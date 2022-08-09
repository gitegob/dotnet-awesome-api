using System.Net;
using System.Text.Json;
using Dotnet_API.Dto;
using Dotnet_API.Exceptions;

namespace Dotnet_API.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                HttpException e => e.StatusCode,
                _ => (int)HttpStatusCode.InternalServerError
            };
            _logger.LogError(error.StackTrace);
            var result =
                JsonSerializer.Serialize(new ApiResponse<Object>(error.Message));
            await response.WriteAsync(result);
        }
    }
}