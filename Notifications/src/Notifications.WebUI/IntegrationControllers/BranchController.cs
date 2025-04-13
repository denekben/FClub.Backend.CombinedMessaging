using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.IntegrationUseCases.Branches;

namespace Notifications.WebUI.IntegrationControllers
{
    [ApiController]
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
