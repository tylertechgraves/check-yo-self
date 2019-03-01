using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;
using paycheck_calculator_web.Server;
using paycheck_calculator_web.Server.Extensions;

namespace paycheck_calculator_web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) => 
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => {
                    var appsettingsConfig = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddEnvironmentVariables()
                        .Build();

                    if (hostingContext.HostingEnvironment.IsDevelopment()) {
                        // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                        config.AddUserSecrets<Startup>();
                    }
                })
                .ConfigureLogging((hostingContext, logging) => {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {                        
                        logging.AddConsole();
                    }                    
                    Server.Startup.Serilog.Setup(hostingContext);
                    logging.AddSerilog(dispose: true);
                })
                .UseConfiguration(
                  new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("hosting.json", optional: true)
                    .AddCommandLine(args)
                    .AddEnvironmentVariables()
                    .Build()
                )
                .UseStartup<Startup>()
                .Build();
    }
}
