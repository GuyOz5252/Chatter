namespace SharedKernel;

public abstract class EntityBase
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    public List<IDomainEvent> DomainEvents => [.. _domainEvents];
    public Guid Id { get; init; }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
