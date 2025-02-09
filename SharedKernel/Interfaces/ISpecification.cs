namespace SharedKernel.Interfaces;

public interface ISpecification<T>
{
   IQueryable<T> Query { get; init; }
   
   public IEnumerable<T> Apply(IEnumerable<T> query);
   
   public bool Apply(T entity);
}
