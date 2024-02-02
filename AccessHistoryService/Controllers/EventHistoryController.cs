using AccessHistoryService.Contracts;
using AccessManager.Models.Enum;
using AccessManager.Sso.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AccessHistoryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventHistoryController : ControllerBase
    {
        private readonly IEventHistoryProvider _provider;

        private readonly ILogger<EventHistoryController> _logger;

        public EventHistoryController(ILogger<EventHistoryController> logger,
            IEventHistoryProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }

        [HttpGet("GetDepartmentEventsCount")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> GetDepartmentEventsCount([BindRequired] EventType eventType, [BindRequired][DataType(DataType.Time)] DateTime timeFrom, [BindRequired][DataType(DataType.Time)] DateTime timeTo)
        {
            _logger.LogInformation($"Request GetDepartmentEventsCount");

            try
            {
                var result = await _provider.GetDepartmentEventsCount(eventType, timeFrom, timeTo);

                _logger.LogInformation($"Succesfull GetDepartmentEventsCount");

                var t = JsonConvert.SerializeObject(result);

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'GetDepartmentEventsCount'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetRoomEventsCount")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> GetRoomEventsCount([BindRequired] Guid employeeId, [BindRequired] EventType eventType)
        {
            _logger.LogInformation($"Request GetRoomEventsCount");

            try
            {
                var result = await _provider.GetRoomEventsCount(employeeId, eventType);

                _logger.LogInformation($"Succesfull GetRoomEventsCount");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'GetRoomEventsCount'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetEmployeeEvents")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> GetEmployeeEvents([BindRequired] Guid roomId, [BindRequired] EventType eventType, [BindRequired][DataType(DataType.Time)] DateTime timeFrom, [BindRequired][DataType(DataType.Time)] DateTime timeTo)
        {
            _logger.LogInformation($"Request GetEmployeeEvents");

            try
            {
                var result = await _provider.GetEmployeeEvents(roomId, eventType, timeFrom, timeTo);

                _logger.LogInformation($"Succesfull GetEmployeeEvents");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'GetEmployeeEvents'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
