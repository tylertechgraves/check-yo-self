using System.IO;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using paycheck_calculator_web.Server;
using paycheck_calculator_web.Server.Entities.Config;
using paycheck_calculator_web.Server.Extensions;
using paycheck_calculator_web.Server.Startup;
using Swashbuckle.AspNetCore.Swagger;

namespace paycheck_calculator_web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; set; }
        private IWebHostEnvironment _env { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfig>(Configuration)
                .AddOptions()
                .AddCors(options => 
                {
                    options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
                })
                .AddResponseCompression(options => 
                {
                    options.MimeTypes = DefaultMimeTypes.Get;
                })
                .AddCustomDbContext(Configuration)
                .AddMemoryCache()
                .RegisterCustomServices()
                .AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN")
                .AddCustomizedMvc()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "content", Version = "v1"});
                })
                .AddHttpClient()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "wwwroot/dist";
                });

            //Setup token validation method
            ConfigureTokenValidation(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            if (env.IsProduction())
            {
                app.UseResponseCompression();
            }
            else
            {
                app.AddDevMiddlewares();
            }

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".po"] = "text/plain";

            app.SetupMigrations()
                .UseXsrf()
                .UseCors("AllowAll")
                .UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "i18n")),
                    RequestPath = "/i18n",
                    ContentTypeProvider = provider
                })
                .UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                    // default route for MVC/API controllers
                    // endpoints.MapRoute(
                    //     name: "default",
                    //     template: "{controller=Home}/{action=Index}/{id?}");

                    // // fallback route for anything that does not match an MVC/API controller
                    // // this will load the angular app and allow for the angular routes to work.
                    // routes.MapSpaFallbackRoute(
                    //     name: "spa-fallback",
                    //     defaults: new { controller = "Home", action = "Index" });
                })
                .UseAuthentication()
                // Enable middleware to serve generated Swagger as a JSON endpoint
                .UseSwagger()
                .UseSpaStaticFiles();

                app.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "Client";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });

            IHttpContextAccessor httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            Context.Configure(httpContextAccessor);
        }

        private void ConfigureTokenValidation(IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    // setting to false to promote working in a docker container
                    options.RequireHttpsMetadata = false; // _env.IsProduction();
                }
            );
        }
    }
}
