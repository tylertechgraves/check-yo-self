using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using check_yo_self.Server.Entities.Config;
using check_yo_self_api_client;
using Mapster;

namespace check_yo_self.Server.Controllers.api
{
  [Route("api/[controller]")]
  [AllowAnonymous]
  public class EmployeesController : BaseController
  {
    private readonly ILogger _logger;
    private readonly AppConfig _appConfig;
    private readonly HttpClient _httpClient;
    private EmployeesClient _apiClient;

    public EmployeesController(ILoggerFactory loggerFactory, IOptionsSnapshot<AppConfig> appConfig, IHttpClientFactory httpClientFactory)
    {
      _logger = loggerFactory.CreateLogger<EmployeesController>();
      _appConfig = appConfig.Value;
      _httpClient = httpClientFactory.CreateClient();
      _apiClient = new check_yo_self_api_client.EmployeesClient(_appConfig.CheckYoSelf.EmployeesApiBaseUrl, _httpClient);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<check_yo_self.Server.Entities.Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync()
    {
      try
      {
        var clientEmployees = await _apiClient.GetAllAsync();
        var employees = clientEmployees.Adapt<List<check_yo_self.Server.Entities.Employee>>();

        return Ok(employees);
      }
      catch (Exception e)
      {
        _logger.LogError("Unable to retrieve employees list from employees API: " + e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("{employeeId}")]
    [ProducesResponseType(typeof(check_yo_self.Server.Entities.Employee), StatusCodes.Status200OK)]
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
          var clientEmployee = await _apiClient.GetByIdAsync(employeeId);

          var employee = clientEmployee.Adapt<check_yo_self.Server.Entities.Employee>();

          return Ok(employee);
        }
        catch (Exception ex)
        {
          _logger.LogError(1, ex, "Unable to get employee by id");
          return StatusCode(StatusCodes.Status500InternalServerError);
        }
      }
    }

    [HttpGet("GetByFullName/{firstName}/{lastName}")]
    [ProducesResponseType(typeof(List<check_yo_self.Server.Entities.Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByFullName(string firstName, string lastName)
    {
      try
      {
        var clientEmployees = await _apiClient.GetByFullNameAsync(firstName, lastName);
        var employees = clientEmployees.Adapt<List<check_yo_self.Server.Entities.Employee>>();

        return Ok(employees);
      }
      catch (Exception e)
      {
        _logger.LogError("Unable to query for employees by full name: " + e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("GetByLastName/{lastName}")]
    [ProducesResponseType(typeof(List<check_yo_self.Server.Entities.Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByLastName(string lastName)
    {
      try
      {
        var clientEmployees = await _apiClient.GetByLastNameAsync(lastName);
        var employees = clientEmployees.Adapt<List<check_yo_self.Server.Entities.Employee>>();

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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int employeeId, [FromBody]check_yo_self.Server.Entities.Employee employee)
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

          var clientEmployee = employee.Adapt<check_yo_self_api_client.Employee>();

          try
          {
            await _apiClient.UpdateAsync(employeeId, clientEmployee);
          }
          catch(SwaggerException swaggerException)
          {
            return StatusCode(swaggerException.StatusCode);
          }

          return NoContent();
        }
        catch (Exception ex)
        {
          _logger.LogError(1, ex, "Unable to update the specified employee with id: " + employee.EmployeeId);
          return StatusCode(StatusCodes.Status500InternalServerError);
        }
      }
    }

    [HttpPost]
    [ProducesResponseType(typeof(check_yo_self.Server.Entities.Employee), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody]check_yo_self.Server.Entities.Employee employee)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      else
      {
        try
        {
          var clientEmployee = employee.Adapt<check_yo_self_api_client.Employee>();

          try
          {
            clientEmployee = await _apiClient.PostAsync(clientEmployee);
          }
          catch (SwaggerException swaggerException)
          {
            return StatusCode(swaggerException.StatusCode);
          }

          employee = clientEmployee.Adapt<check_yo_self.Server.Entities.Employee>();
 
          return Created("/" + _appConfig.CheckYoSelf.ListEmployeesEndpoint, employee);
        }
        catch (Exception ex)
        {
          _logger.LogError(1, ex, "Unable to add new employee: " + employee.LastName + ", " + employee.FirstName);
          return StatusCode(StatusCodes.Status500InternalServerError);
        }
      }
    }
  }
}