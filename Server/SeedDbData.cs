using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace paycheck_calculator_web.Server
{
    public class SeedDbData
    {
        readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnv;
        public SeedDbData(IWebHost host, ApplicationDbContext context)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            var serviceScope = services.CreateScope();
            _hostingEnv = serviceScope.ServiceProvider.GetService<IWebHostEnvironment>();
            _context = context;
        }
    }
}
