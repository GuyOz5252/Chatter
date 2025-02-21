using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Infrastructure;

public abstract class EntityFrameworkRepositoryBase<T> : IRepository<T> where T : class, IAggregateRoot
{
    private readonly DbContext _dbContext;

    protected EntityFrameworkRepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<T>> GetBySpecificationAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().AsQueryable().Concat(specification.Query).SingleAsync(cancellationToken);
    }

    public async Task<Result<List<T>>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<Result<List<T>>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().Concat(specification.Query).ToListAsync(cancellationToken);
    }

    public async Task<Result<T>> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        return entityEntry.Entity;
    }

    public async Task<Result<IEnumerable<T>>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var aggregateRoots = entities.ToList();
        await _dbContext.Set<T>().AddRangeAsync(aggregateRoots, cancellationToken);
        return Result<IEnumerable<T>>.Success(aggregateRoots);
    }

    public Task<Result<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = _dbContext.Set<T>().Update(entity);
        return Task.FromResult(Result<T>.Success(entityEntry.Entity));
    }

    public Task<Result> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().UpdateRange(entities);
        return Task.FromResult(Result.Success());
    }

    public Task<Result<T>> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = _dbContext.Set<T>().Remove(entity);
        return Task.FromResult(Result<T>.Success(entityEntry.Entity));
    }

    public Task<Result> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.RemoveRange(entities);
        return Task.FromResult(Result.Success());
    }

    public Task<Result> DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        var entitiesToDelete = _dbContext.Set<T>().AsQueryable().Concat(specification.Query);
        _dbContext.RemoveRange(entitiesToDelete);
        return Task.FromResult(Result.Success());
    }

    public async Task<Result<int>> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().CountAsync(cancellationToken);
    }

    public async Task<Result<int>> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().Concat(specification.Query).CountAsync(cancellationToken);
    }
}