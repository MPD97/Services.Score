using System;
using Convey.CQRS.Events;
using Convey.MessageBrokers;
using Services.Score.Core.Entities;

namespace Services.Score.Application.Events.External
{
    [Message("users")]
    public class UserCreated: IEvent
    {
        public Guid UserId { get; }
        public State State { get; }

        public UserCreated(Guid userId, State state)
        {
            UserId = userId;
            State = state;
        }
    }
}