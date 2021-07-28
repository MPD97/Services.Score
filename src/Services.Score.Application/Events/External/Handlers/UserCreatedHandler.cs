using System.Threading.Tasks;
using Convey.CQRS.Events;
using Services.Score.Application.Exceptions;
using Services.Score.Core.Entities;
using Services.Score.Core.Repositories;

namespace Services.Score.Application.Events.External.Handlers
{
    public class UserCreatedHandler: IEventHandler<UserCreated>
    {
        private readonly IUserScoreRepository _userScoreRepository;

        public UserCreatedHandler(IUserScoreRepository userScoreRepository)
        {
            _userScoreRepository = userScoreRepository;
        }

        public async Task HandleAsync(UserCreated @event)
        {
            if (await _userScoreRepository.ExistsAsync(@event.UserId))
            {
                throw new UserScoreAlreadyExistsException(@event.UserId);
            }
            
            await _userScoreRepository.AddAsync(new UserScore(@event.UserId, 0, null));
        }
    }
}