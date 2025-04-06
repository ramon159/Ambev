using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Ambev.ServiceDefaults.Logging
{
    public static class LogExtensions
    {
        public static TBuilder ConfigureLog<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            var logTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: SystemConsoleTheme.Colored, outputTemplate: logTemplate)
                .WriteTo.File("logs/log-.log", rollingInterval: RollingInterval.Day, outputTemplate: logTemplate)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            return builder;
        }
    }
}
