using System;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Services.Score.Application.DTO;

namespace Services.Score.Application.Services.Route
{
    public interface IRouteServiceClient
    {
        Task<RouteDto> GetAsync(Guid id);

        Task<PagedRouteDto> GetPagedAsync(bool onlyAccepted = true, string sortOrder = "desc",
            string orderBy = "name", int page = 1);
    }
}