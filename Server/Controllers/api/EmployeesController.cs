using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using check_yo_self_api.Server.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using paycheck_calculator_web.Server.Entities.Config;

namespace paycheck_calculator_web.Server.Controllers.api
{
  [Route("api/[controller]")]
  [AllowAnonymous]
  public class EmployeesController : BaseController
  {
    private readonly ILogger _logger;
    private readonly AppConfig _appConfig;
    private readonly HttpClient _httpClient;

    public EmployeesController(ILoggerFactory loggerFactory, IOptionsSnapshot<AppConfig> appConfig, IHttpClientAccessor httpClientAccessor)
    {
      _logger = loggerFactory.CreateLogger<EmployeesController>();
      _appConfig = appConfig.Value;
      _httpClient = httpClientAccessor.Client;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync()
    {
      try
      {
        var url = _appConfig.CheckYoSelf.EmployeesApiBaseUrl + _appConfig.CheckYoSelf.ListEmployeesEndpoint;

        var employees = await _httpClient.GetStringAsync(url);

        return Ok(employees);
      }
      catch (Exception e)
      {
        _logger.LogError("Unable to retrieve employees list from employees API: " + e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }
  }
}