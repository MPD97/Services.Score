using System;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Services.Score.Application.DTO;
using Services.Score.Application.Queries;
using Services.Score.Infrastructure.Mongo.Documents;

namespace Services.Score.Infrastructure.Mongo.Queries.Handlers
{
    public class GetUserScoreHandler : IQueryHandler<GetUserScore, UserScoreDto>
    {
        private readonly IMongoRepository<UserScoreDocument, Guid> _repository;

        public GetUserScoreHandler(IMongoRepository<UserScoreDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<UserScoreDto> HandleAsync(GetUserScore query)
        {
            var document = await _repository.GetAsync(p => p.Id == query.UserId);

            return document?.AsDto();
        }
    }
}