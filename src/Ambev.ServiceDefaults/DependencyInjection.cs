using Ambev.ServiceDefaults.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace Ambev.ServiceDefaults
{
    public static class DependencyInjection
    {
        public static void UseServiceDefaults(this IHostApplicationBuilder builder)
        {
            builder.ConfigureOpenTelemetry();
            builder.AddDefaultHealthChecks();
            builder.ConfigureLog();
        }
        public static WebApplication MapDefaultEndpoints(this WebApplication app)
        {
            var healthChecks = app.MapGroup("");

            healthChecks
                .CacheOutput("HealthChecks")
                .WithRequestTimeout("HealthChecks");

            healthChecks.MapHealthChecks("/health");

            app.MapHealthChecks("/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live")
            });

            return app;
        }
    }

}
