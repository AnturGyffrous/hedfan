using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyWorkerService.HealthChecks;

namespace MyWorkerService.Services
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;

        private readonly WorkerServiceHealthCheck _healthCheck;

        public WorkerService(ILogger<WorkerService> logger, WorkerServiceHealthCheck healthCheck)
        {
            _logger = logger;
            _healthCheck = healthCheck;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var random = new Random();

            _logger.LogDebug($"{nameof(WorkerService)} is starting.");

            cancellationToken.Register(() => _logger.LogDebug($"{nameof(WorkerService)} is stopping because it's been cancelled."));

            while (!cancellationToken.IsCancellationRequested)
            {
                _healthCheck.Heartbeat();
                _logger.LogDebug($"{nameof(WorkerService)} is doing work.");

                await Task.Delay(random.Next(1000, 30000), cancellationToken);
            }
        }
    }
}