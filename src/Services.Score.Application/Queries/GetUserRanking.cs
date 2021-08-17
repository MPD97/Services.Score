using System;
using Convey.CQRS.Queries;
using Services.Score.Application.DTO;

namespace Services.Score.Application.Queries
{
    public class GetUserRanking : IQuery<UserRankingDto>
    {
        public Guid UserId { get; set; }
    }
}