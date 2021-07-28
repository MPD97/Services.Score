using System;
using Convey.CQRS.Queries;
using Services.Score.Application.DTO;

namespace Services.Score.Application.Queries
{
    public class GetUserScore : IQuery<UserScoreDto>
    {
        public Guid UserId { get; set; }
    }
}