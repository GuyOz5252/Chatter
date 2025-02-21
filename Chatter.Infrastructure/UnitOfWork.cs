using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _dbContext;

    public UnitOfWork(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> CommitAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}