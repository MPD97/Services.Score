using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Services.Score.Application.Events.External
{
    [Message("resources")]
    public class TextResourceCreated: IEvent
    {
        public Guid UserId { get; }
        public Guid RouteId { get; }

        public TextResourceCreated(Guid userId, Guid routeId)
        {
            UserId = userId;
            RouteId = routeId;
        }
    }
}