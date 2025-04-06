using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace Ambev.ServiceDefaults.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static TBuilder UseDefaultHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.Services.AddRequestTimeouts(
                configure: static timeouts =>
                    timeouts.AddPolicy("HealthChecks", TimeSpan.FromSeconds(5)));

            builder.Services.AddOutputCache(
                configureOptions: static caching =>
                    caching.AddPolicy("HealthChecks",
                    build: static policy => policy.Expire(TimeSpan.FromSeconds(10))));

            builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);
            return builder;
        }

    }
}
