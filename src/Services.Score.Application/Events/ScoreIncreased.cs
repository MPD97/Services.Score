using System;
using Convey.CQRS.Events;

namespace Services.Score.Application.Events
{
    [Contract]
    public class ScoreIncreased : IEvent
    {
        public Guid UserId { get; }
        public int AmountAdded { get; }
        public int TotalScore { get; }
        public string Message { get; }

        public ScoreIncreased(Guid userId, int amountAdded, int totalScore, string message)
        {
            UserId = userId;
            AmountAdded = amountAdded;
            TotalScore = totalScore;
            Message = message;
        }
    }
}