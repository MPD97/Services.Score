using System.Collections.Generic;

namespace Services.Score.Core.Entities
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly List<IDomainEvent> _events = new ();
        public IEnumerable<IDomainEvent> Events => _events;
        public AggregateId Id { get; protected set; }
        protected void AddEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }
    }
}