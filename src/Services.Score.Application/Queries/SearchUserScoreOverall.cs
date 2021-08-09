using System.Collections.Generic;
using Convey.CQRS.Queries;
using Services.Score.Application.DTO;

namespace Services.Score.Application.Queries
{
    public class SearchUserScoreOverall :  PagedQueryBase, IQuery<PagedResult<UserScoreOverallDto>>
    {
    }
}