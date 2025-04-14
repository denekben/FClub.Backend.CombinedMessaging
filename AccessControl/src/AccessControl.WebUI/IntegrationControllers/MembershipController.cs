using AccessControl.Application.IntegrationUseCases.Memberships;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.IntegrationControllers
{
    [ApiController]
    [Authorize(Policy = "ManagementIssuer")]
    [Route("api/access-control/internal/memberships")]
    public class MembershipController : ControllerBase
    {
        private readonly ISender _sender;

        public MembershipController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMembership([FromBody] CreateMembership command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{membershipId:guid}")]
        public async Task<ActionResult> DeleteMembership([FromRoute] DeleteMembership command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateMembership([FromBody] UpdateMembership command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
