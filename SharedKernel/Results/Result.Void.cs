namespace SharedKernel.Results;

public class Result
{
    private readonly Error _error;
    
    public bool IsSuccess { get; protected init; } 
    
    public bool IsFailure => !IsSuccess;

    public Error Error => IsFailure ? _error : throw new InvalidOperationException();

    protected Result()
    {
        _error = null!;
        IsSuccess = true;
    }
    
    protected Result(Error error)
    {
        _error = error;
        IsSuccess = false;
    }
    
    public TResult Match<TResult>(Func<TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Error);
    }

    public static Result Success() => new();
    
    public static Result Failure(Error error) => new(error);
    
    public static implicit operator Result(Error error) => Failure(error);
}
