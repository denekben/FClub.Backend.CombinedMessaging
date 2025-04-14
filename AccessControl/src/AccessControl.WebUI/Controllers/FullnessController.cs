using AccessControl.Application.UseCases.Branches.Queries;
using AccessControl.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
    [Route("api/access-control/fullness")]
    public class FullnessController : ControllerBase
    {
        private readonly ISender _sender;

        public FullnessController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<BranchDto>?>> GetBranchesFullness([FromQuery] GetBranchesFullness query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("{branchId:guid}")]
        public async Task<ActionResult<BranchDto?>> GetBranchFullness([FromRoute] Guid branchId)
        {
            var query = new GetBranchFullness(branchId);
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
