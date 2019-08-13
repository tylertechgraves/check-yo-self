using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using paycheck_calculator_web.Server.Entities.Config;
using check_yo_self_api_client;
using Mapster;
using Nest;
using System.Linq;

namespace paycheck_calculator_web.Server.Controllers.api
{
  [Route("api/[controller]")]
  [AllowAnonymous]
  public class EmployeesController : BaseController
  {
    private readonly ILogger _logger;
    private readonly AppConfig _appConfig;
    private readonly HttpClient _httpClient;
    private IElasticClient _elasticClient;

    public EmployeesController(ILoggerFactory loggerFactory, IOptionsSnapshot<AppConfig> appConfig, IHttpClientFactory httpClientFactory)
    {
      _logger = loggerFactory.CreateLogger<EmployeesController>();
      _appConfig = appConfig.Value;
      _httpClient = httpClientFactory.CreateClient();

      var node = new Uri(_appConfig.Elasticsearch.Uri);
      var elasticSettings = new ConnectionSettings(node);

      if (_appConfig.Elasticsearch.UseAuthentication)
      {
          _logger.LogDebug("We did not skip basic auth");
          elasticSettings.BasicAuthentication(_appConfig.Elasticsearch.Username, _appConfig.Elasticsearch.Password);
      }
      else
        _logger.LogDebug("We skipped basic auth");

      _elasticClient = new ElasticClient(elasticSettings);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<check_yo_self_api.Server.Entities.Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync()
    {
      try
      {
        // Search for the document using the employeeId field.             
        var searchResponse = await _elasticClient.SearchAsync<Employee>(s => s
            .Index(_appConfig.Elasticsearch.IndexName)
            .Query(q => q
                .MatchAll()
            )
        );

        // If found, delete the document
        if (searchResponse.IsValid)
        {
            return Ok(searchResponse.Documents);
        }
        else
        {
            return Ok(new List<check_yo_self_api.Server.Entities.Employee>());
        }
      }
      catch (Exception e)
      {
        _logger.LogError("Unable to retrieve employees list from employees API: " + e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }
    // public async Task<IActionResult> GetAsync()
    // {
    //   try
    //   {
    //     var apiClient = new check_yo_self_api_client.EmployeesClient(_appConfig.CheckYoSelf.EmployeesApiBaseUrl, _httpClient);
    //     // var url = _appConfig.CheckYoSelf.EmployeesApiBaseUrl + _appConfig.CheckYoSelf.ListEmployeesEndpoint;

    //     var clientEmployees = await apiClient.GetAllAsync();
    //     var employees = clientEmployees.Adapt<List<check_yo_self_api.Server.Entities.Employee>>();

    //     return Ok(employees);
    //   }
    //   catch (Exception e)
    //   {
    //     _logger.LogError("Unable to retrieve employees list from employees API: " + e.Message);
    //     return StatusCode(StatusCodes.Status500InternalServerError);
    //   }
    // }

    [HttpGet("{employeeId}")]
    [ProducesResponseType(typeof(check_yo_self_api.Server.Entities.Employee), StatusCodes.Status200OK)]
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
          var apiClient = new check_yo_self_api_client.EmployeesClient(_appConfig.CheckYoSelf.EmployeesApiBaseUrl, _httpClient);
          // var url = _appConfig.CheckYoSelf.EmployeesApiBaseUrl + _appConfig.CheckYoSelf.ListEmployeesEndpoint;
          // url = Uri.EscapeUriString(url + "/" + employeeId);

          var clientEmployee = await apiClient.GetByIdAsync(employeeId);

          var employee = clientEmployee.Adapt<check_yo_self_api.Server.Entities.Employee>();

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
    [ProducesResponseType(typeof(List<check_yo_self_api.Server.Entities.Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByFullName(string firstName, string lastName)
    {
      try
      {
        var apiClient = new check_yo_self_api_client.EmployeesClient(_appConfig.CheckYoSelf.EmployeesApiBaseUrl, _httpClient);

        // var url = _appConfig.CheckYoSelf.EmployeesApiBaseUrl + _appConfig.CheckYoSelf.QueryForEmployeesByFullNameEndpoint;
        // url = Uri.EscapeUriString(url + "/" + firstName + "/" + lastName);

        var clientEmployees = await apiClient.GetByFullNameAsync(firstName, lastName);
        var employees = clientEmployees.Adapt<List<check_yo_self_api.Server.Entities.Employee>>();

        return Ok(employees);
      }
      catch (Exception e)
      {
        _logger.LogError("Unable to query for employees by full name: " + e.Message);
        return StatusCode(StatusCodes.Status500InternalServerError);
      }
    }

    [HttpGet("GetByLastName/{lastName}")]
    [ProducesResponseType(typeof(List<check_yo_self_api.Server.Entities.Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByLastName(string lastName)
    {
      try
      {
        var apiClient = new check_yo_self_api_client.EmployeesClient(_appConfig.CheckYoSelf.EmployeesApiBaseUrl, _httpClient);

        // var url = _appConfig.CheckYoSelf.EmployeesApiBaseUrl + _appConfig.CheckYoSelf.QueryForEmployeesByLastNameEndpoint;
        // url = Uri.EscapeUriString(url + "/" + lastName);

        var clientEmployees = await apiClient.GetByLastNameAsync(lastName);
        var employees = clientEmployees.Adapt<List<check_yo_self_api.Server.Entities.Employee>>();

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
    public async Task<IActionResult> Update(int employeeId, [FromBody]check_yo_self_api.Server.Entities.Employee employee)
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

          var apiClient = new check_yo_self_api_client.EmployeesClient(_appConfig.CheckYoSelf.EmployeesApiBaseUrl, _httpClient);

          // var url = _appConfig.CheckYoSelf.EmployeesApiBaseUrl + _appConfig.CheckYoSelf.ListEmployeesEndpoint;
          // url = Uri.EscapeUriString(url + "/" + employeeId);

          // var content = new StringContent(JsonConvert.SerializeObject(employee));
          var clientEmployee = employee.Adapt<check_yo_self_api_client.Employee>();
          // MediaTypeHeaderValue headerValue = new MediaTypeHeaderValue("application/json");
          // content.Headers.ContentType = headerValue;

          try
          {
            await apiClient.UpdateAsync(employeeId, clientEmployee);
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
    [ProducesResponseType(typeof(check_yo_self_api.Server.Entities.Employee), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody]check_yo_self_api.Server.Entities.Employee employee)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      else
      {
        try
        {
          var apiClient = new check_yo_self_api_client.EmployeesClient(_appConfig.CheckYoSelf.EmployeesApiBaseUrl, _httpClient);
          
          // var url = _appConfig.CheckYoSelf.EmployeesApiBaseUrl + _appConfig.CheckYoSelf.ListEmployeesEndpoint;
          // var content = new StringContent(JsonConvert.SerializeObject(employee));
          // MediaTypeHeaderValue headerValue = new MediaTypeHeaderValue("application/json");
          // content.Headers.ContentType = headerValue;

          var clientEmployee = employee.Adapt<check_yo_self_api_client.Employee>();

          try
          {
            clientEmployee = await apiClient.PostAsync(clientEmployee);
          }
          catch (SwaggerException swaggerException)
          {
            return StatusCode(swaggerException.StatusCode);
          }

          employee = clientEmployee.Adapt<check_yo_self_api.Server.Entities.Employee>();
 
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