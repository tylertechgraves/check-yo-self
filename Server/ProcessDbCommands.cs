using System;
using System.Linq;
using paycheck_calculator_web.Server.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace paycheck_calculator_web.Server
{
    public class ProcessDbCommands
    {
        public static void Process(string[] args, IWebHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            var seedService = (SeedDbData)host.Services.GetService(typeof(SeedDbData));

            using (var scope = services.CreateScope())
            {
                var db = GetApplicationDbContext(scope);
                if (args.Contains("dropdb"))
                {
                    Console.WriteLine("Dropping database");
                    db.Database.EnsureDeleted();
                }

                // if (args.Contains("migratedb"))
                // {
                Console.WriteLine("Migrating database");
                db.Database.Migrate();
                // }

                // if (args.Contains("seeddb"))
                // {
                Console.WriteLine("Seeding database");
                db.Seed(host);
                // }
            }
        }

        private static ApplicationDbContext GetApplicationDbContext(IServiceScope services)
        {
            var db = services.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return db;
        }
    }
}