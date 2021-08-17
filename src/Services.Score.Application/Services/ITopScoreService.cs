using System;
using System.Threading.Tasks;

namespace Services.Score.Application.Services
{
    public interface ITopScoreService
    {
        Task AwardPoints(DateTime date);
    }
}