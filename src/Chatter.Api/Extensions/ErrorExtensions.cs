using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using SharedKernel.Results;

namespace Chatter.Api.Extensions;

public static class ErrorExtensions
{
    public static IResult ToProblemDetails(this Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = "An error occurred",
            Type = error.Type.GetDisplayName(),
            Detail = error.Message
        };
        
        return Results.Problem(problemDetails);
    }
}
