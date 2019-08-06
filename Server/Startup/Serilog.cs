using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Hosting;

namespace paycheck_calculator_web.Server.Startup
{
    public static class Serilog
    {
        public static void Setup(WebHostBuilderContext hostingContext)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.RollingFile("logs/log-{Date}.txt", LogEventLevel.Information);

            Log.Logger = log.CreateLogger();
        }
    }
}