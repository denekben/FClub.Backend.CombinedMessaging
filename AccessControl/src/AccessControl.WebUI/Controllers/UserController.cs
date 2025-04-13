using AccessControl.Application.UseCases.UserLogs.Queries;
using AccessControl.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/access-control/users")]
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
        public async Task<ActionResult<List<UserLogDto>?>> GetCurrentUserLogs([FromQuery] GetUserLogs query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
