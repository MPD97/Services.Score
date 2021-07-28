using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Services.Score.Core.Entities;

namespace Services.Score.Application.Events.External
{
    [Message("routes")]
    public class RouteStatusChanged : IEvent
    {
        public Guid RouteId { get; }
        public Status CurrentStatus { get; }
        public Status PreviousStatus { get; }

        public RouteStatusChanged(Guid routeId, Status currentStatus, Status previousStatus)
        {
            RouteId = routeId;
            CurrentStatus = currentStatus;
            PreviousStatus = previousStatus;
        }
    }
}