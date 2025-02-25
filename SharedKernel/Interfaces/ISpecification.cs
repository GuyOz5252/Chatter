using System.Linq.Expressions;

namespace SharedKernel.Interfaces;

public interface ISpecification<T>
{
   Expression<Func<T, bool>> Query { get; init; }
   
   public IQueryable<T> Apply(IEnumerable<T> query);
   
   public bool Apply(T entity);
}
