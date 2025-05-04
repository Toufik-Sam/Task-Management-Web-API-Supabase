using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TaskManagementAPI.Middlewares.CustomExceptions;

namespace TaskManagementAPI.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ErrorHandlingMiddleware(RequestDelegate next,ILogger<ErrorHandlingMiddleware>logger,IHostEnvironment env)
    {
        this._next = next;
        this._logger = logger;
        this._env = env;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");

            context.Response.ContentType = "application/problem+json";
            var statusCode = MapExceptionToStatusCode(ex);

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitleForStatusCode(statusCode),
                Detail = _env.IsDevelopment() ? ex.ToString() : null
            };

            context.Response.StatusCode = statusCode;
            var json = JsonSerializer.Serialize(problem);

            await context.Response.WriteAsync(json);
        }
    }
    private static int MapExceptionToStatusCode(Exception ex)
    {

        return ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetTitleForStatusCode(int statusCode) => statusCode switch
    {
        400 => "Bad Request",
        401 => "Unauthorized",
        404 => "Not Found",
        500 => "Internal Server Error",
        _ => "An error occurred"
    };
}
