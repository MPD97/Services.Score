namespace Services.Score.Application.DTO
{
    public class PagedRouteDto{
        public RouteDto[] Items { get; set; }
        public bool IsEmpty { get; set; }
        public bool IsNotEmpty { get; set; }
        public int CurrentPage { get; set; }
        public int ResultsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

        public PagedRouteDto(RouteDto[] items, bool isEmpty, bool isNotEmpty, int currentPage, int resultsPerPage, int totalPages, int totalResults)
        {
            Items = items;
            IsEmpty = isEmpty;
            IsNotEmpty = isNotEmpty;
            CurrentPage = currentPage;
            ResultsPerPage = resultsPerPage;
            TotalPages = totalPages;
            TotalResults = totalResults;
        }
    }
}