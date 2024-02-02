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
    public class RoomController : ControllerBase
    {
        private readonly IRoomProvider _provider;

        private readonly ILogger<RoomController> _logger;

        public RoomController(ILogger<RoomController> logger,
            IRoomProvider provider)
        {
            _logger = logger;
            _provider = provider;
        }


        [HttpPost("Room")]
        [AuthorizeCustom(UserRole.Admin)]
        public async Task<IActionResult> AddRoom(AddRoomRequest request)
        {
            _logger.LogInformation($"Request Add Room: '{JsonConvert.SerializeObject(request)}'");

            try
            {
                var id = await _provider.AddRoom((RoomInfo)request);

                _logger.LogInformation($"Succesfull Add Room request: '{id}'");

                return new ObjectResult(id);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'Add Room': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("Room")]
        [AuthorizeCustom(UserRole.Admin, UserRole.Staff)]
        public async Task<IActionResult> GetRoom([BindRequired] Guid id)
        {
            _logger.LogInformation($"Request Get Room: '{id}'");

            try
            {
                var result = await _provider.GetRoom(id);

                _logger.LogInformation($"Succesfull Get Room request: '{id}'");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'Get Room': '{e.Message}'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpGet("Rooms")]
        [AuthorizeCustom(UserRole.Admin, UserRole.Staff)]
        public async Task<IActionResult> GetRooms()
        {
            _logger.LogInformation($"Request Get Rooms");

            try
            {
                var result = await _provider.GetRooms();

                _logger.LogInformation($"Succesfull Get Rooms");

                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error during 'Get Rooms'");

                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
