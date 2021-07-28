using System;
using System.Threading.Tasks;
using Services.Score.Core.Entities;

namespace Services.Score.Core.Repositories
{
    public interface IUserScoreRepository
    {
        Task<bool> ExistsAsync(Guid id);
        Task<UserScore> GetAsync(Guid id);
        Task AddAsync(UserScore userScore);
        Task UpdateAsync(UserScore userScore);
    }
}