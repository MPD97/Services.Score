using System;
using System.Collections.Generic;
using System.Linq;
using Convey.CQRS.Events;
using Services.Score.Application.Services;
using Services.Score.Core;
using Services.Score.Core.Events;

namespace Services.Score.Infrastructure.Services
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);

        public IEvent Map(IDomainEvent @event)
        {
            switch (@event)
            {
                case ScoreAdded e: return new Application.Events.ScoreIncreased(e.UserId, e.AmountAdded, e.TotalScore, e.Message);
                    
                default: throw new NotImplementedException();
            }

            return null;
        }
    }
}