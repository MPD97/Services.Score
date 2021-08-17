using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Services.Score.Application;
using Services.Score.Application.DTO;
using Services.Score.Application.Exceptions;
using Services.Score.Application.Services;
using Services.Score.Application.Services.Route;
using Services.Score.Application.Services.Run;
using Services.Score.Core.Entities;
using Services.Score.Core.Repositories;

namespace Services.Score.Infrastructure.Services
{
    public class TopScoreService : ITopScoreService
    {
        private readonly IRunServiceClient _runClient;
        private readonly IRouteServiceClient _routeClient;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserScoreRepository _userScoreRepository;
        private readonly IEventMapper _eventMapper;
        private readonly IMessageBroker _messageBroker;

        public TopScoreService(IRunServiceClient runClient, IRouteServiceClient routeClient,
            IDateTimeProvider dateTimeProvider, IUserScoreRepository userScoreRepository, IEventMapper eventMapper,
            IMessageBroker messageBroker)
        {
            _runClient = runClient;
            _routeClient = routeClient;
            _dateTimeProvider = dateTimeProvider;
            _userScoreRepository = userScoreRepository;
            _eventMapper = eventMapper;
            _messageBroker = messageBroker;
        }

        public async Task AwardPoints(DateTime date)
        {
            var page = 0;
            var totalPages = 1;

            while (page < totalPages)
            {
                var pagedRouteResult = await _routeClient.GetPagedAsync(onlyAccepted: true, sortOrder: "desc",
                    orderBy: "name", page: ++page);

                totalPages = pagedRouteResult.TotalPages;

                foreach (var route in pagedRouteResult.Items)
                {
                    await GetBestPlayers(route.Id, date);
                }
            }
        }

        private async Task GetBestPlayers(Guid routeId, DateTime date)
        {
            var rankingId = new HashSet<Guid>();

            var page = 0;
            var totalPages = 1;
            while (page < totalPages && rankingId.Count < 10)
            {
                var pagedRunScoreResult = await _runClient.GetPagedAsync(routeId,
                    date.AddMonths(-1), sortOrder: "asc", orderBy: "time", ++page);
                
                totalPages = pagedRunScoreResult.TotalPages;

                foreach (var runRanking in pagedRunScoreResult.Items)
                {
                    rankingId.Add(runRanking.UserId);
                }
            }

            for (int i = 0; i < rankingId.Count; i++)
            {
                if (i >= 10)
                    break;
                
                var userId = rankingId.ElementAt(i);

                var userScore = await _userScoreRepository.GetAsync(userId);
                if (userScore is null)
                {
                    Log.Error("UserScore was not found for user with id: {UserId}", userId);
                    return;
                }

                ScoreEvent scoreEvent = null;
                switch (i)
                {
                    case 0:
                        scoreEvent = ScoreEvent.ScoreRouteFirst(Guid.NewGuid(), _dateTimeProvider.Now, routeId, date);
                        break;
                    case 1:
                        scoreEvent = ScoreEvent.ScoreRouteSecond(Guid.NewGuid(), _dateTimeProvider.Now, routeId, date);
                        break;
                    case 2:
                        scoreEvent = ScoreEvent.ScoreRouteThird(Guid.NewGuid(), _dateTimeProvider.Now, routeId, date);
                        break;
                    case >= 2:
                        scoreEvent = ScoreEvent.ScoreRouteTopTen(Guid.NewGuid(), _dateTimeProvider.Now, routeId, date);
                        break;
                }
                
                if (userScore.IsIncreasable(scoreEvent) == false)
                {
                    Log.Warning("UserScore was not increasable for user with id: {UserId}", userId);
                    return;
                }

                userScore.AddScore(scoreEvent);
                await _userScoreRepository.UpdateAsync(userScore);

                var events = _eventMapper.MapAll(userScore.Events);
                await _messageBroker.PublishAsync(events);
            }
        }
    }
}