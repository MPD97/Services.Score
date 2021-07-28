using System;
using System.Collections.Generic;
using Convey.Types;

namespace Services.Score.Infrastructure.Mongo.Documents
{
    public class UserScoreDocument: IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public IEnumerable<ScoreEventDocument> ScoreEvents { get; set; }

        public UserScoreDocument()
        {
        }

        public UserScoreDocument(Guid id, int score, IEnumerable<ScoreEventDocument> scoreEvents)
        {
            Id = id;
            Score = score;
            ScoreEvents = scoreEvents;
        }
    }
}