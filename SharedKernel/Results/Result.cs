namespace SharedKernel.Results;

public sealed class Result<T> : Result
{
    private readonly T _value;
    
    public T Value => IsSuccess ? _value : throw new InvalidOperationException();

    private Result(T value)
    {
        _value = value;
        IsSuccess = true;
    }

    private Result(Error error) : base(error)
    {
        _value = default!;
    }
    
    public new TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(Error);
    }
    
    public static Result<T> Success(T value) => new(value);
    
    public new static Result<T> Failure(Error error) => new(error); 
    
    public static implicit operator T(Result<T> result) => result.Value;
    
    public static implicit operator Result<T>(T value) => new(value);
    
    public static implicit operator Result<T>(Error error) => Failure(error);
}
