using System;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using Services.Score.Core.Entities;
using Services.Score.Core.Repositories;
using Services.Score.Infrastructure.Mongo.Documents;

namespace Services.Score.Infrastructure.Mongo.Repositories
{
    public class UserScoreMongoRepository : IUserScoreRepository
    {
        private readonly IMongoRepository<UserScoreDocument, Guid> _repository;

        public UserScoreMongoRepository(IMongoRepository<UserScoreDocument, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExistsAsync(Guid id)
            => await _repository.ExistsAsync(u => u.Id == id);

        public async Task<UserScore> GetAsync(Guid id)
        {
            var userScore = await _repository.GetAsync(r => r.Id == id);
    
            return userScore?.AsEntity();
        }

        public async Task AddAsync(UserScore userScore)
            => await _repository.AddAsync(userScore.AsDocument());

        public async Task UpdateAsync(UserScore userScore)
            => await _repository.UpdateAsync(userScore.AsDocument());

    }
}