namespace SharedKernel.Results;

public record Error(ErrorType ErrorType, string Message)
{
    // TODO: Test what happens if string format doesnt have a format
    public static Error NotFound(string nameof, string message = "{0} not found") =>
        new(ErrorType.NotFound, string.Format(message, nameof));

    public static Error Conflict(string nameof, string message = "Conflict: {0} already exists") => 
        new(ErrorType.Conflict, string.Format(message, nameof));
    
    public static Error Unauthorized() => 
        new(ErrorType.Unauthorized, "Unauthorized");
    
    public static Error Forbidden(string message) => 
        new(ErrorType.Forbidden, $"Forbidden: {message}");
}
