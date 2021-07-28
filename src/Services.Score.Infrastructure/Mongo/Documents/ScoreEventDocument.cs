using System;
using Services.Score.Core.Entities;

namespace Services.Score.Infrastructure.Mongo.Documents
{
    public class ScoreEventDocument
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatesAt { get; set; }
        public int Amount { get; set; }
        public ScoreType Type { get; set; }
        public Guid? RouteId { get; set; }
        public DateTime? Date { get; set; }

        public ScoreEventDocument()
        {
        }

        public ScoreEventDocument(Guid id, string message, DateTime createsAt, int amount, ScoreType type, Guid? routeId, DateTime? date)
        {
            Id = id;
            Message = message;
            CreatesAt = createsAt;
            Amount = amount;
            Type = type;
            RouteId = routeId;
            Date = date;
            Date = date;
        }
    }
}