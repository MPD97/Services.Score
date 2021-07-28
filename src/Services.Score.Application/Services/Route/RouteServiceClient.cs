using System;
using System.Threading.Tasks;
using Services.Score.Application.DTO;

namespace Services.Score.Application.Services.Route
{
    public interface IRouteServiceClient
    {
        Task<RouteDto> GetAsync(Guid id);
    }
}