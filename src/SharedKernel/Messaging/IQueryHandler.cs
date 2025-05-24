using SharedKernel.Results;

namespace SharedKernel.Messaging;

public interface IQueryHandler<in TQuery, TOutput> where TQuery : IQuery<TOutput>
{
    Task<Result<TOutput>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
