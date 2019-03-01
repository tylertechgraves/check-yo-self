using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IO;
using paycheck_calculator_web.Server.Entities.Config;

namespace paycheck_calculator_web.Server.Controllers
{
    public class SignoutController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly AppConfig _appConfig;

        public SignoutController(IHostingEnvironment env, IOptionsSnapshot<AppConfig> appConfig)
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
