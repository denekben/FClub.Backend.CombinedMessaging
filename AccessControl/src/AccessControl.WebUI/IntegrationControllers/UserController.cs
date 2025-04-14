using AccessControl.Application.IntegrationUseCases.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.IntegrationControllers
{
    [ApiController]
    [Authorize(Policy = "ManagementIssuer")]
    [Route("api/notifications/internal/users")]
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
    }
}
