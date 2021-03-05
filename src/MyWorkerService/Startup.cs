using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWorkerService.HealthChecks;
using MyWorkerService.Services;

namespace MyWorkerService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<WorkerServiceHealthCheck>();

            services.AddHealthChecks().AddCheck<WorkerServiceHealthCheck>(nameof(WorkerServiceHealthCheck));
            services.AddHostedService<WorkerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/");
                endpoints.MapGet("/stop",
                    async context =>
                    {
                        var workerService = endpoints.ServiceProvider.GetServices<IHostedService>().FirstOrDefault(x => x is WorkerService);
                        if (workerService != null)
                        {
                            var cancellationTokenSource = new CancellationTokenSource();
                            var task = workerService.StopAsync(CancellationToken.None);
                            await Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(5), CancellationToken.None));
                            cancellationTokenSource.Cancel();
                        }
                        await context.Response.WriteAsync($"Stopped {nameof(WorkerService)}");
                    });
            });
        }
    }
}
