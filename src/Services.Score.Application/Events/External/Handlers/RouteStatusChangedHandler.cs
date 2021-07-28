using System;
using System.Threading.Tasks;
using Convey.CQRS.Events;
using Services.Score.Application.Exceptions;
using Services.Score.Application.Services;
using Services.Score.Application.Services.Route;
using Services.Score.Core.Entities;
using Services.Score.Core.Repositories;

namespace Services.Score.Application.Events.External.Handlers
{
    public class  RouteStatusChangedHandler: IEventHandler<RouteStatusChanged>
    {
        private readonly IUserScoreRepository _userScoreRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;
        private readonly IRouteServiceClient _routeServiceClient;

        public RouteStatusChangedHandler(IUserScoreRepository userScoreRepository, IDateTimeProvider dateTimeProvider,
            IEventMapper eventMapper, IMessageBroker messageBroker, IRouteServiceClient routeServiceClient)
        {
            _userScoreRepository = userScoreRepository;
            _dateTimeProvider = dateTimeProvider;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
            _routeServiceClient = routeServiceClient;
        }

        public async Task HandleAsync(RouteStatusChanged @event)
        {
            var route = await _routeServiceClient.GetAsync(@event.RouteId);
            if (route is null)
                throw new RouteNotFoundException(@event.RouteId);
            
            var userScore = await _userScoreRepository.GetAsync(route.UserId);
            if (userScore is null)
                throw new UserScoreNotFoundException(route.UserId);

            userScore.AddScore(ScoreEvent.ScoreRouteAdded(Guid.NewGuid(), _dateTimeProvider.Now, @event.RouteId));
            await _userScoreRepository.UpdateAsync(userScore);
            
            var events = _eventMapper.MapAll(userScore.Events);
            await _messageBroker.PublishAsync(events);
        }
    }
}