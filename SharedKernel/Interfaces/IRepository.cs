using SharedKernel.Results;

namespace SharedKernel.Interfaces;

public interface IRepository<T> where T : class, IAggregateRoot
{
    Task<Result<T>> GetByPk(object pk, CancellationToken cancellationToken = default);
    
    Task<Result<T>> GetBySpecificationAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    
    Task<Result<List<T>>> ListAsync(CancellationToken cancellationToken = default);
    
    Task<Result<List<T>>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    
    Task<Result<T>> AddAsync(T entity, CancellationToken cancellationToken = default);
    
    Task<Result<IEnumerable<T>>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    Task<Result<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    Task<Result> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    Task<Result<T>> DeleteAsync(T entity, CancellationToken cancellationToken = default);
    
    Task<Result> DeleteRangeAsync(IEnumerable<T> specification, CancellationToken cancellationToken = default);
    
    Task<Result> DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    
    Task<Result<int>> CountAsync(CancellationToken cancellationToken = default);
    
    Task<Result<int>> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
}
