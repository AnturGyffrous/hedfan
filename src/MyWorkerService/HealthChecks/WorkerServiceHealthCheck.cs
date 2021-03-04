using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MyWorkerService.HealthChecks
{
    public class WorkerServiceHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            //return Task.FromResult(HealthCheckResult.Degraded("Not so healthy if I'm honest"));
            return Task.FromResult(HealthCheckResult.Unhealthy("Really quite ill"));
        }
    }
}
