using System;
using System.Collections.Generic;
using Services.Score.Core.Entities;

namespace Services.Score.Application.DTO
{
    public class UserScoreDto
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public IEnumerable<ScoreEventDto> ScoreEvents { get; set; }

        public UserScoreDto()
        {
        }

        public UserScoreDto(Guid id, int score, IEnumerable<ScoreEventDto> scoreEvents)
        {
            Id = id;
            Score = score;
            ScoreEvents = scoreEvents;
        }

        public class ScoreEventDto 
        {
            public Guid Id { get; set; }
            public string Message { get; set; }
            public DateTime CreatesAt { get; set; }
            public int Amount { get; set; }
            public ScoreType Type { get; set; }
            public Guid? RouteId { get; set; }
            public DateTime? Date { get; set; }

            public ScoreEventDto(Guid id, string message, DateTime createsAt, int amount, ScoreType type, Guid? routeId, DateTime? date)
            {
                Id = id;
                Message = message;
                CreatesAt = createsAt;
                Amount = amount;
                Type = type;
                RouteId = routeId;
                Date = date;
            }
        }
    }
}