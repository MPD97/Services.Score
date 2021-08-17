using Newtonsoft.Json;

namespace Services.Score.Application.DTO
{
    public class PagedRunRankingDto
    {
        [JsonProperty("items")]
        public RunRankingDto[] Items { get; set; }
        
        [JsonProperty("isEmpty")]
        public bool IsEmpty { get; set; }

        [JsonProperty("isNotEmpty")]
        public bool IsNotEmpty { get; set; }

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }

        [JsonProperty("resultsPerPage")]
        public int ResultsPerPage { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }

        public PagedRunRankingDto(RunRankingDto[] items, bool isEmpty, bool isNotEmpty, int currentPage, int resultsPerPage, int totalPages, int totalResults)
        {
            Items = items;
            IsEmpty = isEmpty;
            IsNotEmpty = isNotEmpty;
            CurrentPage = currentPage;
            ResultsPerPage = resultsPerPage;
            TotalPages = totalPages;
            TotalResults = totalResults;
        }

        public PagedRunRankingDto()
        {
                
        }
    }
}