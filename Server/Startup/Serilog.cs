using Serilog;
using Serilog.Events;
using Microsoft.ApplicationInsights.AspNetCore;
using Serilog.Sinks.ApplicationInsights;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace paycheck_calculator_web.Server.Startup
{
    public static class Serilog
    {
        public static void Setup(WebHostBuilderContext hostingContext)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.RollingFile("logs/log-{Date}.txt", LogEventLevel.Information);

            var instrumentationKey = hostingContext.Configuration.GetValue<string>("ApplicationInsights:InstrumentationKey");
            if (!string.IsNullOrWhiteSpace(instrumentationKey) && instrumentationKey != System.Guid.Empty.ToString())
            {
                var logEventLevel = hostingContext.HostingEnvironment.IsDevelopment() ? LogEventLevel.Information : LogEventLevel.Error;
                log.WriteTo.ApplicationInsightsEvents(instrumentationKey, logEventLevel);
            }

            Log.Logger = log.CreateLogger();
        }
    }
}