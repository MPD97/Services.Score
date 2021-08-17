using System;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using Services.Score.Application.DTO;
using Services.Score.Application.Queries;
using Services.Score.Infrastructure.Mongo.Documents;

namespace Services.Score.Infrastructure.Mongo.Queries.Handlers
{
    public class GetUserRankingHandler : IQueryHandler<GetUserRanking, UserRankingDto>
    {
        private readonly IMongoRepository<UserScoreDocument, Guid> _repository;

        public GetUserRankingHandler(IMongoRepository<UserScoreDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<UserRankingDto> HandleAsync(GetUserRanking query)
        {
            var scores = await _repository.FindAsync(us => true);
            var place = scores.OrderByDescending(r => r.Score).TakeWhile(x => x.Id != query.UserId).Count() + 1;
            return new UserRankingDto{Id = query.UserId, Place = place};
        }
    }
}