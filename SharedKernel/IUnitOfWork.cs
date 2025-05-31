using SharedKernel.Results;

namespace SharedKernel;

public interface IUnitOfWork
{
    Task<Result> CommitAsync(CancellationToken cancellationToken = default);
}
