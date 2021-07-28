using System.Collections.Generic;
using Convey.CQRS.Events;
using Services.Score.Core;

namespace Services.Score.Application.Services
{
    public interface IEventMapper
    {
        IEvent Map(IDomainEvent @event);
        IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events);
    }
}