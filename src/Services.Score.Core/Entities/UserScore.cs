using System;
using System.Collections.Generic;
using System.Linq;
using Services.Score.Core.Events;
using Services.Score.Core.Exceptions;

namespace Services.Score.Core.Entities
{
    public class UserScore : AggregateRoot
    {
        private const int MinimumAmount = 0;
        private const int MaximumAmount = int.MaxValue;
        private ISet<ScoreEvent> _scoreEvents = new HashSet<ScoreEvent>();
        public int Score { get; private set; }
        
        public IEnumerable<ScoreEvent> ScoreEvents {   
            get => _scoreEvents;
            private set => _scoreEvents = new HashSet<ScoreEvent>(value); 
        }

        public UserScore(Guid id, int score, IEnumerable<ScoreEvent> scoreEvents)
        {
            Id = id;
            Score = score is < MinimumAmount or > MaximumAmount ?
                throw new InvalidScoreAmountException(score, MinimumAmount, MaximumAmount) 
                : score;
            ScoreEvents = scoreEvents ?? Enumerable.Empty<ScoreEvent>();
        }

        public bool IsIncreasable(ScoreEvent @event)
        {
            switch (@event.Type)        
            {
                case ScoreType.RouteAdded:
                    return ScoreEvents.Where(se => se.Type == ScoreType.RouteAdded)
                        .FirstOrDefault(se => se.RouteId == @event.RouteId) is null;
                case ScoreType.RouteCompleted:
                    return true;
                case ScoreType.RouteTopTen:
                    return ScoreEvents.Where(se => se.Type == ScoreType.RouteTopTen)
                        .Where(se => se.Date == @event.Date)
                        .FirstOrDefault(se => se.RouteId == @event.RouteId) is null;
                case ScoreType.RouteThird:
                    return ScoreEvents.Where(se => se.Type == ScoreType.RouteThird)
                        .Where(se => se.Date == @event.Date)
                        .FirstOrDefault(se => se.RouteId == @event.RouteId) is null;
                case ScoreType.RouteSecond:
                    return ScoreEvents.Where(se => se.Type == ScoreType.RouteSecond)
                        .Where(se => se.Date == @event.Date)
                        .FirstOrDefault(se => se.RouteId == @event.RouteId) is null;
                case ScoreType.RouteFirst:
                    return ScoreEvents.Where(se => se.Type == ScoreType.RouteFirst)
                        .Where(se => se.Date == @event.Date)
                        .FirstOrDefault(se => se.RouteId == @event.RouteId) is null;
                case ScoreType.RouteCommented:
                    return ScoreEvents.Where(se => se.Type == ScoreType.RouteCommented)
                        .FirstOrDefault(se => se.RouteId == @event.RouteId) is null;
                case ScoreType.RouteCommentedWithPhoto:
                    return ScoreEvents.Where(se => se.Type == ScoreType.RouteCommentedWithPhoto)
                        .FirstOrDefault(se => se.RouteId == @event.RouteId) is null;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public void AddScore(ScoreEvent @event)
        {
            if (!IsIncreasable(@event))
                throw new UserScoreCannotBeIncreasedException(Id);
            
            _scoreEvents.Add(@event);
            UpdateScore();
            AddEvent(new ScoreAdded(Id, @event.Amount, Score, @event.Message));
        }

        private void UpdateScore()
        {
            Score = _scoreEvents.Sum(se => se.Amount);
        }
    }
}