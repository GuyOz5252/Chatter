using Microsoft.EntityFrameworkCore;
using SharedKernel.Interfaces;
using SharedKernel.Results;

namespace Chatter.Infrastructure;

public abstract class EntityFrameworkRepositoryBase<T> : IRepository<T> where T : class, IAggregateRoot
{
    protected readonly DbContext DbContext;

    protected EntityFrameworkRepositoryBase(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<Result<T>> GetByPk(object pk, CancellationToken cancellationToken = default)
    {
        return (await DbContext.Set<T>().FindAsync([pk], cancellationToken: cancellationToken))!;
    }

    public virtual async Task<Result<T>> GetBySpecificationAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return (await DbContext.Set<T>().FirstOrDefaultAsync(specification.Query, cancellationToken))!;
    }

    public virtual  async Task<Result<List<T>>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public virtual  async Task<Result<List<T>>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().Where(specification.Query).ToListAsync(cancellationToken);
    }

    public virtual  async Task<Result<T>> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = await DbContext.Set<T>().AddAsync(entity, cancellationToken);
        return entityEntry.Entity;
    }

    public virtual  async Task<Result<IEnumerable<T>>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        var aggregateRoots = entities.ToList();
        await DbContext.Set<T>().AddRangeAsync(aggregateRoots, cancellationToken);
        return Result<IEnumerable<T>>.Success(aggregateRoots);
    }

    public virtual  Task<Result<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = DbContext.Set<T>().Update(entity);
        return Task.FromResult(Result<T>.Success(entityEntry.Entity));
    }

    public virtual  Task<Result> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<T>().UpdateRange(entities);
        return Task.FromResult(Result.Success());
    }

    public virtual  Task<Result<T>> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        var entityEntry = DbContext.Set<T>().Remove(entity);
        return Task.FromResult(Result<T>.Success(entityEntry.Entity));
    }

    public virtual  Task<Result> DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        DbContext.RemoveRange(entities);
        return Task.FromResult(Result.Success());
    }

    public virtual  Task<Result> DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        var entitiesToDelete = DbContext.Set<T>().Where(specification.Query);
        DbContext.RemoveRange(entitiesToDelete);
        return Task.FromResult(Result.Success());
    }

    public virtual  async Task<Result<int>> CountAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().CountAsync(cancellationToken);
    }

    public virtual  async Task<Result<int>> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<T>().Where(specification.Query).AsQueryable().CountAsync(cancellationToken);
    }
}