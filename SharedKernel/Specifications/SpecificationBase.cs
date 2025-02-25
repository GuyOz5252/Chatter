using System.Linq.Expressions;
using SharedKernel.Interfaces;

namespace SharedKernel.Specifications;

public abstract class SpecificationBase<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Query { get; init; } = _ => true;

    public virtual IQueryable<T> Apply(IEnumerable<T> query)
    {
        return query
            .Where(Query.Compile())
            .AsQueryable();
    }

    public virtual bool Apply(T entity)
    {
        return Query.Compile().Invoke(entity);
    }
}