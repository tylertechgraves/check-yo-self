using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using paycheck_calculator_web.Server;
using paycheck_calculator_web.Server.Startup;
using paycheck_calculator_web.Server.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using paycheck_calculator_web.Server.Entities;
using paycheck_calculator_web.Server.Entities.Config;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace paycheck_calculator_web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; set; }
        private IHostingEnvironment _env { get; set; }


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
                      .AllowAnyMethod()
                      .AllowCredentials());
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
                    c.SwaggerDoc("v1", new Info { Title = "content", Version = "v1" });
                })
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<IHttpClientAccessor, DefaultHttpClientAccessor>()
                .AddNodeServices(); // added last because it returns void and breaks the fluent API

            //Setup token validation method
            ConfigureTokenValidation(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
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
                .UseStaticFiles()
                .UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "i18n")),
                    RequestPath = "/i18n",
                    ContentTypeProvider = provider
                })
                .UseAuthentication()
                // Enable middleware to serve generated Swagger as a JSON endpoint
                .UseSwagger()
                .UseMvc(routes =>
                {
                    // default route for MVC/API controllers
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");

                    // fallback route for anything that does not match an MVC/API controller
                    // this will load the angular app and allow for the angular routes to work.
                    routes.MapSpaFallbackRoute(
                        name: "spa-fallback",
                        defaults: new { controller = "Home", action = "Index" });
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
                });
        }

    }
}
