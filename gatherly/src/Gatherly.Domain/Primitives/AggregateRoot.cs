namespace Gatherly.Domain.Primitives;

public abstract class AggregateRoot(Guid id) : Entity(id)
{
    private readonly List<IDomainEvent> _domainEvents = [];

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}