using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using paycheck_calculator_web.Server.Entities.Config;

namespace paycheck_calculator_web.Server.Controllers.api
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AppConfigController : BaseController
    {
        private readonly ILogger _logger;
        private readonly AppConfig _appConfig;

        public AppConfigController(IOptionsSnapshot<AppConfig> appConfig, ILoggerFactory loggerFactory) 
        {
            _logger = loggerFactory.CreateLogger<AppConfigController>();
            _appConfig = appConfig.Value;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok(_appConfig);
        }
    }
}