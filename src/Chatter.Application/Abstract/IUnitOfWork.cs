using SharedKernel;

namespace Chatter.Application.Abstract;

public interface IUnitOfWork
{
    Task<Result> CommitAsync(CancellationToken cancellationToken = default);
}
