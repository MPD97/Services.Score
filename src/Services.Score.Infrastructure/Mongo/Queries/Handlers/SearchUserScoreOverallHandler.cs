using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Services.Score.Application.DTO;
using Services.Score.Application.Queries;
using Services.Score.Infrastructure.Mongo.Documents;

namespace Services.Score.Infrastructure.Mongo.Queries.Handlers
{
    public class SearchUserScoreOverallHandler : IQueryHandler<SearchUserScoreOverall, PagedResult<UserScoreOverallDto>>
    {
        private readonly IMongoRepository<UserScoreDocument, Guid> _repository;

        public SearchUserScoreOverallHandler(IMongoRepository<UserScoreDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<UserScoreOverallDto>> HandleAsync(SearchUserScoreOverall query)
        {
            var pagedResult = await _repository.BrowseAsync(_ => true, query);
            return pagedResult?.Map(d => d.AsScoreOverallDto()); 
        }
    }
}