using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MyWorkerService.Services
{
    public abstract class BackgroundService : IHostedService, IDisposable
    {
        private Task _task;

        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _task = ExecuteAsync(_cancellationTokenSource.Token);

            return _task.IsCompleted
                ? _task
                : Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_task == null)
            {
                return;
            }

            try
            {
                _cancellationTokenSource.Cancel();
            }
            finally
            {
                await Task.WhenAny(_task, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}