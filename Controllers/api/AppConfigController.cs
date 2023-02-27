using check_yo_self.Server.Entities.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace check_yo_self.Server.Controllers.api;

[Route("api/[controller]")]
[AllowAnonymous]
public class AppConfigController : BaseController
{
    private readonly AppConfig _appConfig;

    public AppConfigController(IOptionsSnapshot<AppConfig> appConfig)
    {
        _appConfig = appConfig.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_appConfig);
    }
}
