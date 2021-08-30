using System.Collections.Generic;

namespace Services.Score.Core.Entities
{
    public interface IAggregateRoot
    {
        IEnumerable<IDomainEvent> Events { get; }
        AggregateId Id { get;  }
    }
}