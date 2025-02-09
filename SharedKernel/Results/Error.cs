namespace SharedKernel.Results;

public record Error(ErrorType ErrorType, string Message)
{
    // TODO: Test what happens if string format doesnt have a format
    public static Error NotFound(string nameof, string message = "{0} not found") =>
        new(ErrorType.NotFound, string.Format(message, nameof));
}
