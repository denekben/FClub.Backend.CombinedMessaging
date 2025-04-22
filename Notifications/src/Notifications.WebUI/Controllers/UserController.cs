using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.UseCases.UserLogs.Queries;
using Notifications.Domain.DTOs;

namespace Notifications.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
    [Route("api/notifications/users")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("current/logs")]
        public async Task<ActionResult<List<UserLogDto>?>> GetCurrentUserLogs([FromQuery] GetCurrentUserLogs query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("logs")]
        public async Task<ActionResult<List<UserLogDto>?>> GetUserLogs([FromQuery] GetUserLogs query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
