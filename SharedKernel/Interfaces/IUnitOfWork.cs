using SharedKernel.Results;

namespace SharedKernel.Interfaces;

public interface IUnitOfWork
{
    Task<Result> CommitAsync(CancellationToken cancellationToken = default);
}