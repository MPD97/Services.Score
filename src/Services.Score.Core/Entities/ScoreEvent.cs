using System;
using System.Globalization;
using Services.Score.Core.Exceptions;

namespace Services.Score.Core.Entities
{
    public class ScoreEvent
    {
        private const int MinimumAmount = 1;
        private const int MaximumAmount = 500;
        private static readonly CultureInfo CultureInfo = new ("en-US");

        public AggregateId Id { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatesAt { get; private set; }
        public int Amount { get; private set; }
        public ScoreType Type { get; private set; }
        public Guid? RouteId { get; private set; }
        public DateTime? Date { get; private set; }

        public ScoreEvent(AggregateId id, string message, DateTime createsAt, int amount,
            ScoreType type, Guid? routeId, DateTime? date)
        {
            Id = id;
            Message = string.IsNullOrWhiteSpace(message) ? throw new InvalidScoreMessageException(message) : message;
            CreatesAt = createsAt;
            Amount = amount is < MinimumAmount or > MaximumAmount ? 
                throw new InvalidScoreAmountException(amount, MinimumAmount, MaximumAmount) 
                : amount;
            Type = type;
            RouteId = routeId;
            Date = date;
        }

        public static ScoreEvent ScoreRouteAdded(Guid id, DateTime createdAt, Guid routeId)
            => new ScoreEvent(id, "Route added", createdAt, 5, ScoreType.RouteAdded, routeId, null);
        
        public static ScoreEvent ScoreRouteCompleted(Guid id, DateTime createdAt, Guid routeId)
            => new ScoreEvent(id, "Route completed", createdAt, 5, ScoreType.RouteCompleted, routeId, null);
        
        public static ScoreEvent ScoreRouteTopTen(Guid id, DateTime createdAt, Guid routeId, DateTime forDate)
            => new ScoreEvent(id, $"Route top 10 in {forDate.ToString("Y", CultureInfo)}", createdAt,
                10, ScoreType.RouteTopTen, routeId, forDate);
        
        public static ScoreEvent ScoreRouteThird(Guid id, DateTime createdAt, Guid routeId, DateTime forDate)
            => new ScoreEvent(id, $"Route third place in {forDate.ToString("Y", CultureInfo)}", createdAt,
                15, ScoreType.RouteThird, routeId, forDate);
        
        public static ScoreEvent ScoreRouteSecond(Guid id, DateTime createdAt, Guid routeId, DateTime forDate)
            => new ScoreEvent(id, $"Route second place in {forDate.ToString("Y", CultureInfo)}", createdAt,
                25, ScoreType.RouteSecond, routeId, forDate);
        
        public static ScoreEvent ScoreRouteFirst(Guid id, DateTime createdAt, Guid routeId, DateTime forDate)
            => new ScoreEvent(id, $"Route first place in {forDate.ToString("Y", CultureInfo)}", createdAt,
                35, ScoreType.RouteFirst, routeId, forDate);
        
        public static ScoreEvent ScoreRouteCommented(Guid id, DateTime createdAt, Guid routeId)
            => new ScoreEvent(id, $"Route commented", createdAt,
                2, ScoreType.RouteCommented, routeId, null);
        
        public static ScoreEvent ScoreRouteCommentedWithPhoto(Guid id, DateTime createdAt, Guid routeId)
            => new ScoreEvent(id, $"Route commented with photo", createdAt,
                5, ScoreType.RouteCommentedWithPhoto, routeId, null);
    }
}