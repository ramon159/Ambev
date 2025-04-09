using Ambev.Shared.Common.Exceptions;
using Ambev.Shared.Common.Http;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Diagnostics;

namespace Ambev.Api.Middlewares;

/// <summary>
/// Handler that manages exceptions
/// </summary>
public class CustomExceptionHandler : IExceptionHandler
{
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

    /// <summary>
    /// Constructor of CustomExceptionHandler
    /// </summary>
    public CustomExceptionHandler()
    {
        _exceptionHandlers = new()
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(ForbiddenAccessException), HandleForbiddenAccessException },
            };
    }

    /// <summary>
    /// method to sync the correct exception to respective handler
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="exception"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();

        if (_exceptionHandlers.ContainsKey(exceptionType))
        {
            await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
            return true;
        }

        await HandleUnknownExceptionAsync(httpContext, exception);
        return true;
    }

    private async Task HandleUnknownExceptionAsync(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(new ApiErrorResponse
        {
            Status = httpContext.Response.StatusCode,
            Type = "InternalServerError",
            Error = "An unexpected error occurred.",
            Detail = ex.Message
        });
    }

    private async Task HandleValidationException(HttpContext httpContext, Exception ex)
    {
        var exception = (ValidationException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(new ApiValidationProblemDetails(exception.Errors)
        {
            Status = httpContext.Response.StatusCode,
            Type = "ValidationError",
            Error= "Invalid input data",
            Detail = ex.Message
        });
    }

    private async Task HandleNotFoundException(HttpContext httpContext, Exception ex)
    {
        var exception = (NotFoundException)ex;

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

        await httpContext.Response.WriteAsJsonAsync(new ApiErrorResponse()
        {
            Status = httpContext.Response.StatusCode,
            Type = "ResourceNotFound",
            Error = "The specified resource was not found.",
            Detail = exception.Message
        });
    }

    private async Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

        await httpContext.Response.WriteAsJsonAsync(new ApiErrorResponse
        {
            Status = httpContext.Response.StatusCode,
            Type = "AuthenticationError",
            Error = "Invalid authentication token",
            Detail= ex.Message
        });
    }

    private async Task HandleForbiddenAccessException(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

        await httpContext.Response.WriteAsJsonAsync(new ApiErrorResponse
        {
            Status = httpContext.Response.StatusCode,
            Error = "Forbidden",
            Type = "ForbiddenError"
        });
    }
}
