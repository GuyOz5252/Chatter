using SharedKernel.Interfaces;

namespace SharedKernel.Specifications;

public abstract class SpecificationBase<T> : ISpecification<T>
{
    public IQueryable<T> Query { get; init; } = Enumerable.Empty<T>().AsQueryable();

    public IEnumerable<T> Apply(IEnumerable<T> query)
    {
        return query
            .Concat(Query)
            .AsEnumerable();
    }

    public bool Apply(T entity)
    {
        return new[] { entity }
            .Concat(Query)
            .AsEnumerable()
            .Contains(entity);
    }
}