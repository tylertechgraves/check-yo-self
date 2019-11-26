using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using paycheck_calculator_web.Server.Entities.Config;

namespace paycheck_calculator_web.Server.Controllers
{
    public class SignoutController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppConfig _appConfig;

        public SignoutController(IWebHostEnvironment env, IOptionsSnapshot<AppConfig> appConfig)
        {
            _env = env;
            _appConfig =  appConfig.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
