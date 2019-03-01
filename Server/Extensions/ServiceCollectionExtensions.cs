using System.IO;
using System.Security.Cryptography.X509Certificates;
using paycheck_calculator_web.Server.Entities;
using paycheck_calculator_web.Server.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace paycheck_calculator_web.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {       
        public static IServiceCollection AddCustomizedMvc(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ModelValidationFilter));
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            return services;
        }        
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // Add framework services.
            // services.AddDbContext<ApplicationDbContext>(options =>
            // {
            //     string useSqLite = configuration["Data:useSqLite"];
            //     if (useSqLite.ToLower() == "true")
            //     {
            //         options.UseSqlite(configuration["Data:SqlLiteConnectionString"]);
            //     }
            //     else
            //     {
            //         options.UseSqlServer(configuration["Data:SqlServerConnectionString"]);
            //     }
            // });
            return services;
        }
        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ApiExceptionFilter>();
            return services;
        }
    }
}
