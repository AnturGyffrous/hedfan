using System;
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
                return Task.FromResult(HealthCheckResult.Unhealthy($"Last heartbeat: {timespan}"));
            }

            if (timespan.TotalSeconds > 5)
            {
                return Task.FromResult(HealthCheckResult.Degraded($"Last heartbeat: {timespan}"));
            }

            return Task.FromResult(HealthCheckResult.Healthy());
        }

        public void Heartbeat()
        {
            Interlocked.Exchange(ref _lastHeartBeat, DateTime.UtcNow.Ticks);
        }
    }
}
