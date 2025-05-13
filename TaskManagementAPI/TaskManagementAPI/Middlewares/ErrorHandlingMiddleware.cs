using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TaskManagementDataAccessLayer.CustomSupabaseClient;
using TaskManagementDataAccessLayer.Exceptions;

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
    public async Task Invoke(HttpContext context, ITokenAccessor tokenAccessor)
    {
        try
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();
            if(!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer "))
                tokenAccessor.Token = authHeader.Substring("Bearer ".Length).Trim();
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
            BadRequestException => StatusCodes.Status400BadRequest,
            BadHttpRequestException=>StatusCodes.Status400BadRequest,
            ForbiddenRequestException=>StatusCodes.Status403Forbidden,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetTitleForStatusCode(int statusCode) => statusCode switch
    {
        400 => "Bad Request",
        401 => "Unauthorized",
        403=>  "Forbidden",
        404 => "Not Found",
        500 => "Internal Server Error",
        _ => "An error occurred"
    };
}
