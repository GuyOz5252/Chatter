namespace SharedKernel.Results;

public record Error
{
    public string Message { get; }
    public ErrorType Type { get; }
    
    public Error(string message, ErrorType type)
    {
        Message = message;
        Type = type;
    }

    public static Error None() =>
        new(string.Empty, ErrorType.Failure);
    
    public static Error Failure(string message) =>
        new(message, ErrorType.Failure);

    public static Error NotFound(string message) =>
        new(message, ErrorType.NotFound);

    public static Error Problem(string message) =>
        new(message, ErrorType.Problem);

    public static Error Conflict(string message) =>
        new(message, ErrorType.Conflict);
    
    public static Error Validation(string message) =>
        new(message, ErrorType.Validation);

    public static Error Unauthorized(string message) =>
        new(message, ErrorType.Unauthorized);
}
