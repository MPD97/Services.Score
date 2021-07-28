using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Services.Score.Application.Exceptions;
using Services.Score.Application.Services;
using Services.Score.Core.Entities;
using Services.Score.Core.Repositories;

namespace Services.Score.Application.Events.External.Handlers
{
    public class RunCompletedHandler : IEventHandler<RunCompleted>
    {
        private readonly IUserScoreRepository _userScoreRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;

        public RunCompletedHandler(IUserScoreRepository userScoreRepository, IDateTimeProvider dateTimeProvider,
            IEventMapper eventMapper, IMessageBroker messageBroker)
        {
            _userScoreRepository = userScoreRepository;
            _dateTimeProvider = dateTimeProvider;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
        }

        public async Task HandleAsync(RunCompleted @event)
        {
            var userScore = await _userScoreRepository.GetAsync(@event.UserId);
            if (userScore is null)
            {
                throw new UserScoreNotFoundException(@event.UserId);
            }
            
            userScore.AddScore(ScoreEvent.ScoreRouteCompleted(Guid.NewGuid(), _dateTimeProvider.Now, @event.RouteId));
            await _userScoreRepository.UpdateAsync(userScore);
            
            var events = _eventMapper.MapAll(userScore.Events);
            await _messageBroker.PublishAsync(events);
        }
    }
}