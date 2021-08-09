using System.Linq;
using Services.Score.Application.DTO;
using Services.Score.Core.Entities;

namespace Services.Score.Infrastructure.Mongo.Documents
{
    public static class Extensions
    {
        public static UserScore AsEntity(this UserScoreDocument document)
            => new UserScore(document.Id, document.Score, document.ScoreEvents.Select(se =>
                new ScoreEvent(se.Id, se.Message, se.CreatesAt, se.Amount, se.Type, se.RouteId, se.Date)));

        public static UserScoreDocument AsDocument(this UserScore entity)
            => new UserScoreDocument
            {
                Id = entity.Id,
                Score = entity.Score,
                ScoreEvents = entity.ScoreEvents.Select(se 
                    => new ScoreEventDocument(se.Id, se.Message, se.CreatesAt, se.Amount, se.Type, se.RouteId, se.Date))
            };

        public static UserScoreDto AsDto(this UserScoreDocument document)
            => new UserScoreDto
            {
                Id = document.Id,
                Score = document.Score,
                ScoreEvents = document.ScoreEvents.Select(se
                    => new UserScoreDto.ScoreEventDto(se.Id, se.Message,se.CreatesAt,se.Amount,se.Type,se.RouteId,se.Date))
               
            };
        
        public static UserScoreOverallDto AsScoreOverallDto(this UserScoreDocument document)
            => new UserScoreOverallDto
            {
                Id = document.Id,
                Score = document.Score,
            };
    }
}