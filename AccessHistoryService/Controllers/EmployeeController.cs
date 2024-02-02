using AccessHistoryService.Contracts;
using AccessManager.Models.DataModels;
using AccessManager.Models.Enum;
using AccessManager.Models.Requests.AccessHistory;
using AccessManager.Sso.Attributes;
using AccessManager.Sso.Attributes.FromClaimAttribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AccessHistoryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeProvider _provider;

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger,
            IEmployeeProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }


        [HttpPost("AddEmployee")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest request)
        {
            _logger.LogInformation($"Request AddEmployee: '{JsonConvert.SerializeObject(request)}'");

            try
            {
                var employeeId = await _provider.AddEmployee((EmployeeInfo)request);

                _logger.LogInformation($"Succesfull AddEmployee request: '{employeeId}'");

                return new ObjectResult(employeeId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'AddEmployee': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetEmployeeInfo")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> GetEmployee([BindRequired] Guid employeeId)
        {
            _logger.LogInformation($"Request GetEmployee: '{employeeId}'");

            try
            {
                var result = await _provider.GetEmployee(employeeId);

                _logger.LogInformation($"Succesfull GetEmployee request: '{employeeId}'");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'GetEmployee': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetMyEmployeeInfo")]
        [AuthorizeCustom(UserRole.Staff, UserRole.Admin)]
        public async Task<IActionResult> GetMyEmployeeInfo([FromClaim("Id")] Guid employeeId)
        {
            _logger.LogInformation($"Request GetEmployee: '{employeeId}'");

            try
            {
                var result = await _provider.GetEmployee(employeeId);

                _logger.LogInformation($"Succesfull GetEmployee request: '{employeeId}'");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'GetEmployee': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("Employees")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> GetEmployees()
        {
            _logger.LogInformation($"Request GetEmployees");

            try
            {
                var result = await _provider.GetEmployees();

                _logger.LogInformation($"Succesfull GetEmployees");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'GetEmployees'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
