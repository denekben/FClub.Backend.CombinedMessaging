using Management.Application.UseCases.Memberships.Commands;
using Management.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebUI.Controllers
{
    [ApiController]
    [Route("api/management/memberships")]
    public class MembershipController : ControllerBase
    {
        private readonly ISender _sender;

        public MembershipController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<MembershipDto?>> CreateMembership([FromBody] CreateMembership command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{membershipId:guid}")]
        public async Task<ActionResult> DeleteMembership([FromRoute] DeleteMembership command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<MembershipDto?>> UpdateMembership([FromBody] UpdateMembership command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
