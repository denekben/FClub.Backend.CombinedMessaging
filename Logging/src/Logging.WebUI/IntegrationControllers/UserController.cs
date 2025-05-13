using Logging.Application.IntegrationUseCases.AppUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.IntegrationUseCases.AppUsers;

namespace Logging.WebUI.IntegrationControllers
{
    [ApiController]
    [Authorize(Policy = "ManagementIssuer")]
    [Route("api/logging/internal/users")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut]
        [Route("{userId:guid}/block")]
        public async Task<ActionResult> BlockUser([FromRoute] Guid userId)
        {
            await _sender.Send(new BlockUser(userId));
            return Ok();
        }

        [HttpPut]
        [Route("{userId:guid}/unblock")]
        public async Task<ActionResult> UnblockUser([FromRoute] Guid userId)
        {
            await _sender.Send(new UnblockUser(userId));
            return Ok();
        }

        [HttpPost]
        [Route("register-user")]
        public async Task<ActionResult> RegisterNewUser([FromBody] RegisterNewUser command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
