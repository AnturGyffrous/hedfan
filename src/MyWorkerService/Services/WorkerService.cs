using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyWorkerService.Services
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;

        public WorkerService(ILogger<WorkerService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var random = new Random();

            _logger.LogDebug($"{nameof(WorkerService)} is starting.");

            cancellationToken.Register(() => _logger.LogDebug($"{nameof(WorkerService)} is stopping because it's been cancelled."));

            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogDebug($"{nameof(WorkerService)} is doing work.");

                await Task.Delay(random.Next(10000, 30000), cancellationToken);
            }

        }
    }
}