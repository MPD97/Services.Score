using System;
using System.Threading.Tasks;
using Convey.HTTP;
using Services.Score.Application.DTO;
using Services.Score.Application.Services.Route;

namespace Services.Score.Infrastructure.Services.Route
{
    internal sealed class RouteServiceClient: IRouteServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public RouteServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["route"];
        }

        public Task<RouteDto> GetAsync(Guid id)
            => _httpClient.GetAsync<RouteDto>($"{_url}/routes/{id}");
    }
}