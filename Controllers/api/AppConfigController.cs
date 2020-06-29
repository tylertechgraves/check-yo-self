using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using check_yo_self.Server.Entities.Config;

namespace check_yo_self.Server.Controllers.api
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