using System;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Services.Score.Application.DTO;

namespace Services.Score.Application.Services.Run
{
    public interface IRunServiceClient
    {
        Task<PagedRunRankingDto> GetPagedAsync(Guid routeId, DateTime month, string sortOrder = "asc",
            string orderBy = "time", int page = 1);
    }
}