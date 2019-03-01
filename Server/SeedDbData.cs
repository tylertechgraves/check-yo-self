using System;
using System.Collections.Generic;
using System.Linq;
using paycheck_calculator_web.Server.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace paycheck_calculator_web.Server
{
    public class SeedDbData
    {
        readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;
        public SeedDbData(IWebHost host, ApplicationDbContext context)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            var serviceScope = services.CreateScope();
            _hostingEnv = serviceScope.ServiceProvider.GetService<IHostingEnvironment>();
            _context = context;
        }
    }
}
