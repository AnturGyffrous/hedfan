using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MyWorkerService.HealthChecks
{
    public class WorkerServiceHealthCheck : IHealthCheck
    {
        private long _lastHeartBeat;

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var lastHeartBeat = new DateTime(Interlocked.Read(ref _lastHeartBeat));
            var timespan = DateTime.UtcNow - lastHeartBeat;

            if (timespan.TotalSeconds > 20)
            {
                var message = new StringBuilder("It has been ");
                if (timespan.Days == 1) 
                {
                    message.Append("1 day ");
                }
                else if (timespan.Days > 1)
                {
                    message.Append($"{timespan.Days} days ");
                }

                if (timespan.Hours == 1)
                {
                    message.Append("1 hour ");
                }
                else if (timespan.Hours > 1)
                {
                    message.Append($"{timespan.Hours} hours ");
                }

                if (timespan.Minutes == 1)
                {
                    message.Append("1 minute ");
                }
                else if (timespan.Minutes > 1)
                {
                    message.Append($"{timespan.Minutes} minutes ");
                }
                
                if (timespan.Seconds == 1)
                {
                    message.Append("1 second");
                }
                else
                {
                    message.Append($"{timespan.Seconds} seconds");
                }

                return Task.FromResult(HealthCheckResult.Unhealthy($"{message} since the last heartbeat."));
            }

            return Task.FromResult(HealthCheckResult.Healthy());
        }

        public void Heartbeat()
        {
            Interlocked.Exchange(ref _lastHeartBeat, DateTime.UtcNow.Ticks);
        }
    }
}
