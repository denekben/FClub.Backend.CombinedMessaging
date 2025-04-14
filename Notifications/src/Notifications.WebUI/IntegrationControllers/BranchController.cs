using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.IntegrationUseCases.Branches;

namespace Notifications.WebUI.IntegrationControllers
{
    [ApiController]
    [Authorize(Policy = "ManagementIssuer")]
    [Route("api/notifications/internal/branches")]
    public class BranchController : ControllerBase
    {
        private readonly ISender _sender;

        public BranchController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBranch([FromBody] CreateBranch command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
