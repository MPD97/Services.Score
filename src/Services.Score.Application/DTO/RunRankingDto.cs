using System;

namespace Services.Score.Application.DTO
{
    public class RunRankingDto
    {
        public Guid UserId { get; set; }
        public DateTimeOffset RunDate { get; set; }
        public DateTimeOffset Time { get; set; }

        public RunRankingDto(Guid userId, DateTimeOffset runDate, DateTimeOffset time)
        {
            UserId = userId;
            RunDate = runDate;
            Time = time;
        }
    }
}