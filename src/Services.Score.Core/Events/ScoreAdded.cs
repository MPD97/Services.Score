using System;

namespace Services.Score.Core.Events
{
    public class ScoreAdded : IDomainEvent
    {
        public Guid UserId { get; }
        public int AmountAdded { get; }
        public int TotalScore { get; }
        public string Message { get; }

        public ScoreAdded(Guid userId, int amountAdded, int totalScore, string message)
        {
            UserId = userId;
            AmountAdded = amountAdded;
            TotalScore = totalScore;
            Message = message;
        }
    }
}