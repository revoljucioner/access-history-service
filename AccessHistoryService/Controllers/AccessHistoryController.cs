using AccessHistoryService.Providers;
using AccessManager.Models.Requests.AccessHistory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AccessHistoryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessHistoryController : ControllerBase
    {
        private readonly AccessHistoryManager _manager;

        private readonly ILogger<AccessHistoryController> _logger;

        public AccessHistoryController(ILogger<AccessHistoryController> logger,
            AccessHistoryManager manager)
        {
            _logger = logger;
            _manager = manager;
        }


        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest request)
        {
            _logger.LogInformation($"Request AddEmployee: '{JsonConvert.SerializeObject(request)}'");

            try
            {
                var employeeId = await _manager.AddEmployee(request);

                _logger.LogInformation($"Succesfull AddEmployee request: '{employeeId}'");

                return new ObjectResult(employeeId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'AddEmployee': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment(AddDepartmentRequest request)
        {
            _logger.LogInformation($"Request AddDepartment: '{JsonConvert.SerializeObject(request)}'");

            try
            {
                var departmentId = await _manager.AddDepartment(request);

                _logger.LogInformation($"Succesfull AddDepartment request: '{departmentId}'");

                return new ObjectResult(departmentId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'AddDepartment': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("AddRoom")]
        public async Task<IActionResult> AddRoom(AddRoomRequest request)
        {
            _logger.LogInformation($"Request AddRoom: '{JsonConvert.SerializeObject(request)}'");

            try
            {
                var roomId = await _manager.AddRoom(request);

                _logger.LogInformation($"Succesfull AddRoom request: '{roomId}'");

                return new ObjectResult(roomId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'AddRoom': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("AddEvent")]
        public async Task<IActionResult> AddEvent(AddEventRequest request)
        {
            _logger.LogInformation($"Request AddEvent: '{JsonConvert.SerializeObject(request)}'");

            try
            {
                var eventId = await _manager.AddEvent(request);

                _logger.LogInformation($"Succesfull AddEvent request: '{eventId}'");

                return new ObjectResult(eventId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'AddEvent': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("GetEvents")]
        public async Task<IActionResult> GetEvents(GetEventsRequest request)
        {
            _logger.LogInformation($"Request GetEvents: '{JsonConvert.SerializeObject(request)}'");

            try
            {
                var events = await _manager.GetEvents(request);

                _logger.LogInformation($"Succesfull GetEvents request: '{events}'");

                return new ObjectResult(events);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'GetEvents': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
