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
    public class EventController : ControllerBase
    {
        private readonly IEventProvider _provider;

        private readonly ILogger<EventController> _logger;

        public EventController(ILogger<EventController> logger,
            IEventProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }


        [HttpPost("Event")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> AddEvent(AddEventRequest request)
        {
            _logger.LogInformation($"Request Add Event: '{JsonConvert.SerializeObject(request)}'");

            try
            {
                var id = await _provider.AddEvent((EventInfo)request);

                _logger.LogInformation($"Succesfull Add Event request: '{id}'");

                return new ObjectResult(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'Add Event': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("Event")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> GetEvent([BindRequired] Guid id)
        {
            _logger.LogInformation($"Request Get Event: '{id}'");

            try
            {
                var result = await _provider.GetEvent(id);

                _logger.LogInformation($"Succesfull Get Event request: '{id}'");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'Get Event': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpGet("Events")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> GetEvents()
        {
            _logger.LogInformation($"Request Get Events");

            try
            {
                var result = await _provider.GetEvents();

                _logger.LogInformation($"Succesfull Get Events");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'Get Events'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
