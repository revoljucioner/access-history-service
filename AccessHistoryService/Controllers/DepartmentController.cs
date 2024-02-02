using AccessHistoryService.Contracts;
using AccessManager.Models.DataModels;
using AccessManager.Models.Enum;
using AccessManager.Models.Requests.AccessHistory;
using AccessManager.Sso.Attributes;
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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentProvider _provider;

        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(ILogger<DepartmentController> logger,
            IDepartmentProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }


        [HttpPost("Department")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> AddDepartment(AddDepartmentRequest request)
        {
            _logger.LogInformation($"Request Add Department: '{JsonConvert.SerializeObject(request)}'");

            try
            {
                var id = await _provider.AddDepartment((DepartmentInfo)request);

                _logger.LogInformation($"Succesfull Add Department request: '{id}'");

                return new ObjectResult(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'Add Department': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("Department")]
        [AuthorizeCustom(UserRole.Admin, UserRole.Staff)]
        public async Task<IActionResult> GetDepartment([BindRequired] Guid id)
        {
            _logger.LogInformation($"Request Get Department: '{id}'");

            try
            {
                var result = await _provider.GetDepartment(id);

                _logger.LogInformation($"Succesfull Get Department request: '{id}'");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'Get Department': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpGet("Departments")]
        [AuthorizeCustom(UserRole.Admin, UserRole.Staff)]
        public async Task<IActionResult> GetDepartments()
        {
            _logger.LogInformation($"Request Get Departments");

            try
            {
                var result = await _provider.GetDepartments();

                _logger.LogInformation($"Succesfull Get Departments");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'Get Departments'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
