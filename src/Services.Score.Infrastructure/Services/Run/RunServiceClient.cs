using System;
using System.Threading.Tasks;
using Convey.HTTP;
using Newtonsoft.Json;
using Services.Score.Application.DTO;
using Services.Score.Application.Services.Run;

namespace Services.Score.Infrastructure.Services.Run
{
    public class RunServiceClient : IRunServiceClient
    {
        private readonly IHttpClient _httpClient;
        private readonly string _url;

        public RunServiceClient(IHttpClient httpClient, HttpClientOptions options)
        {
            _httpClient = httpClient;
            _url = options.Services["run"];
        }

        public async Task<PagedRunRankingDto> GetPagedAsync(Guid routeId, DateTime month, string sortOrder = "asc",
            string orderBy = "time", int page = 1)
        {
          var response =  await _httpClient.GetAsync(
                $"{_url}/runs?routeId={routeId}&date={month:s}&orderBy={orderBy}&sortOrder={sortOrder}&page={page}");
          var responseStr = await response.Content.ReadAsStringAsync();
          return JsonConvert.DeserializeObject<PagedRunRankingDto>(responseStr);
        }
    }
}