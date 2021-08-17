using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Services.Score.Application.Services;
using static System.Threading.Tasks.Task;

namespace Services.Score.Infrastructure.BackgroundJobs
{
    public class TopScoreJob : BackgroundService
    {
        private const int DelayTime = 5 * 1000;
        private static bool _started = false;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ITopScoreService _topScoreService;

        public TopScoreJob(IDateTimeProvider dateTimeProvider, ITopScoreService topScoreService)
        {
            _dateTimeProvider = dateTimeProvider;
            _topScoreService = topScoreService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Verbose("Delaying background job of rewarding players for their results in runs");
                await Delay(DelayTime);
                
                var now = _dateTimeProvider.Now;
                var firstDayOfMonth = new DateTime(now.Year, now.Month, 1, 0,0,0);
                if (now.Day == firstDayOfMonth.Day && now.Month == firstDayOfMonth.Month &&
                    now.Year == firstDayOfMonth.Year && _started == false)
                {
                    _started = true;
                    
                    Log.Information("Starting background job of rewarding players for their results in runs");

                    await _topScoreService.AwardPoints(firstDayOfMonth);

                    Log.Information("Completed background job of rewarding players for their results in runs");
                }
                else
                {
                    _started = false;
                }
            }
        }
    }
}