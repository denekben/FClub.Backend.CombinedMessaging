using AccessControl.Application.IntegrationUseCases.Branches;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.IntegrationControllers
{
    [ApiController]
    [Route("api/access-control/internal/branches")]
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

        [HttpDelete]
        [Route("{branchId:guid}")]
        public async Task<ActionResult> DeleteBranch([FromRoute] DeleteBranch command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBranch([FromBody] UpdateBranch command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
