using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using check_yo_self.Server.Entities.Config;
using check_yo_self_api_client;
using check_yo_self_api_client.V1;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace check_yo_self.Server.Controllers.api;

[Route("api/[controller]")]
[AllowAnonymous]
public class EmployeesController : BaseController
{
    private readonly ILogger _logger;
    private readonly AppConfig _appConfig;
    private readonly IEmployeesClient _employeesClient;

    public EmployeesController(ILoggerFactory loggerFactory, IOptionsSnapshot<AppConfig> appConfig, IEmployeesClient employeesClient)
    {
        _logger = loggerFactory.CreateLogger<EmployeesController>();
        _appConfig = appConfig.Value;
        _employeesClient = employeesClient;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<check_yo_self.Server.Entities.Employee>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync()
    {
        try
        {
            var clientEmployees = await _employeesClient.GetAllAsync();
            var employees = clientEmployees.Adapt<List<check_yo_self.Server.Entities.Employee>>();

            return Ok(employees);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to retrieve employees list from employees API: {message}", e.Message);
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
                var clientEmployee = await _employeesClient.GetByIdAsync(employeeId);

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
            var clientEmployees = await _employeesClient.GetByFullNameAsync(firstName, lastName);
            var employees = clientEmployees.Adapt<List<check_yo_self.Server.Entities.Employee>>();

            return Ok(employees);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to query for employees by full name: {message}", e.Message);
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
            var clientEmployees = await _employeesClient.GetByLastNameAsync(lastName);
            var employees = clientEmployees.Adapt<List<check_yo_self.Server.Entities.Employee>>();

            return Ok(employees);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to query for employees by full name: {message}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{employeeId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int employeeId, [FromBody] check_yo_self.Server.Entities.Employee employee)
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

                var clientEmployee = employee.Adapt<check_yo_self_api_client.V1.Employee>();

                try
                {
                    await _employeesClient.UpdateAsync(employeeId, clientEmployee);
                }
                catch (SwaggerException swaggerException)
                {
                    return StatusCode(swaggerException.StatusCode);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to update the specified employee with id: {employeeId}", employee.EmployeeId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(check_yo_self.Server.Entities.Employee), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] check_yo_self.Server.Entities.Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        else
        {
            try
            {
                var clientEmployee = employee.Adapt<check_yo_self_api_client.V1.Employee>();

                try
                {
                    clientEmployee = await _employeesClient.PostAsync(clientEmployee);
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
                _logger.LogError(ex, "Unable to add new employee: {lastName}, {firstName}", employee.LastName, employee.FirstName);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
