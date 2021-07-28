using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;

namespace Services.Score.Application.Events.External
{
    [Message("runs")]
    public class RunCompleted : IEvent
    {
        public Guid RunId { get; }
        public Guid UserId { get; }
        public Guid RouteId { get; }
        
        public RunCompleted(Guid runId, Guid userId, Guid routeId)
        {
            RunId = runId;
            UserId = userId;
            RouteId = routeId;
        }
    }
}