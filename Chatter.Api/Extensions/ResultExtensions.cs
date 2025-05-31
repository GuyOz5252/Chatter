using SharedKernel.Results;

namespace Chatter.Api.Extensions;

public static class ResultExtensions
{
    public static TOutput Match<TOutput>(
        this Result result,
        Func<TOutput> onSuccess,
        Func<Error, TOutput> onFailure)
    {
        return result.IsSuccess ? onSuccess.Invoke() : onFailure.Invoke(result.Error);
    }

    public static TOutput Match<TInput, TOutput>(
        this Result<TInput> result,
        Func<TInput, TOutput> onSuccess,
        Func<Error, TOutput> onFailure)
    {
        return result.IsSuccess ? onSuccess.Invoke(result.Value) : onFailure.Invoke(result.Error);
    }
}
