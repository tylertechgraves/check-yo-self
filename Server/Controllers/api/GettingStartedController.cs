using System;
using System.Linq;
using System.Threading.Tasks;
using paycheck_calculator_web.Server.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace paycheck_calculator_web.Server.Controllers.api
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class GettingStartedController : BaseController
    {
        //private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public GettingStartedController(/*ApplicationDbContext context,*/ ILoggerFactory loggerFactory)
        {
            //_context = context;
            _logger = loggerFactory.CreateLogger<GettingStartedController>();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var gettingStarted = new GettingStarted();
                gettingStarted.ApplicationName = "paycheck_calculator_web";
                return await Task.FromResult(Ok(gettingStarted));
            }
            catch (Exception ex)
            {
                _logger.LogError(1, ex, "Unable to get status");
                return BadRequest();
            }

        }

    }

}
