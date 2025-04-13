using AccessControl.Application.UseCases.Branches.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.Controllers
{
    [ApiController]
    [Route("api/access-control/fullness")]
    public class FullnessController : ControllerBase
    {
        private readonly ISender _sender;

        public FullnessController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<Guid, uint>?>> GetBranchesFullness([FromQuery] GetBranchesFullness query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("{branchId:guid}")]
        public async Task<ActionResult<uint?>> GetBranchFullness([FromRoute] Guid branchId)
        {
            var query = new GetBranchFullness(branchId);
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
