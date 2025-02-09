using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

public class ProblemDetailsExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "An error occurred",
            Type = exception.GetType().Name,
            Detail = exception.Message
        };

        httpContext.Response.StatusCode = statusCode;
        
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}