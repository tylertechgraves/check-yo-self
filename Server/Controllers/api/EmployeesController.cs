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
using Newtonsoft.Json;
using System.Net.Http.Headers;

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

    [HttpGet("{employeeId}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById(int employeeId)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      else
      {
        try
        {
          var url = _appConfig.CheckYoSelf.EmployeesApiBaseUrl + _appConfig.CheckYoSelf.ListEmployeesEndpoint;
          url = Uri.EscapeUriString(url + "/" + employeeId);

          var employee = await _httpClient.GetStringAsync(url);

          return Ok(employee);
        }
        catch (Exception ex)
        {
          _logger.LogError(1, ex, "Unable to get employee by id");
          return StatusCode(StatusCodes.Status500InternalServerError);
        }
      }
    }

    [HttpGet("QueryByFullName/{firstName}/{lastName}")]
    [ProducesResponseType(typeof(List<Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> QueryByFullName(string firstName, string lastName)
    {
      try
      {
        var url = _appConfig.CheckYoSelf.EmployeesApiBaseUrl + _appConfig.CheckYoSelf.QueryForEmployeesByFullNameEndpoint;
        url = Uri.EscapeUriString(url + "/" + firstName + "/" + lastName);

        var employees = await _httpClient.GetStringAsync(url);

        return Ok(employees);
      }
      catch (Exception e)
      {
        _logger.LogError("Unable to query for employees by full name: " + e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpPut("{employeeId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int employeeId, [FromBody]Employee employee)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      else
      {
        try
        {
          if (employeeId != employee.EmployeeId)
            return BadRequest();

          var url = _appConfig.CheckYoSelf.EmployeesApiBaseUrl + _appConfig.CheckYoSelf.ListEmployeesEndpoint;
          url = Uri.EscapeUriString(url + "/" + employeeId);

          var content = new StringContent(JsonConvert.SerializeObject(employee));
          MediaTypeHeaderValue headerValue = new MediaTypeHeaderValue("application/json");
          content.Headers.ContentType = headerValue;

          var response = await _httpClient.PutAsync(url, content);

          if (response.IsSuccessStatusCode)
            return NoContent();
          else
          {
            _logger.LogError("Unable to update the specified employee with id: " + employee.EmployeeId);
            return StatusCode(StatusCodes.Status500InternalServerError);
          }
        }
        catch (Exception ex)
        {
          _logger.LogError(1, ex, "Unable to update the specified employee with id: " + employee.EmployeeId);
          return StatusCode(StatusCodes.Status500InternalServerError);
        }
      }
    }
  }
}