using AccessControl.Application.UseCases.Turnstiles.Commands;
using AccessControl.Application.UseCases.Turnstiles.Queries;
using AccessControl.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.Controllers
{
    [ApiController]
    [Route("api/access-control/turnstiles")]
    public class TurnstileController : ControllerBase
    {
        private readonly ISender _sender;

        public TurnstileController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Authorize(Policy = "IsNotBlocked", Roles = "Admin")]
        public async Task<ActionResult<TurnstileDto?>> CreateTurnstile([FromBody] CreateTurnstile command)
        {
            var result = await _sender.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{turnstileId:guid}")]
        [Authorize(Policy = "IsNotBlocked", Roles = "Admin")]
        public async Task<ActionResult> DeleteTurnstile([FromRoute] Guid turnstileId)
        {
            var command = new DeleteTurnstile(turnstileId);
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        [Route("go-through")]
        public async Task<ActionResult> GoThrough([FromBody] GoThrough command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = "IsNotBlocked", Roles = "Admin")]
        public async Task<ActionResult<TurnstileDto?>> UpdateTurnstile([FromBody] UpdateTurnstile command)
        {
            var result = await _sender.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Route("{turnstileId:guid}")]
        [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
        public async Task<ActionResult<TurnstileDto?>> GetTurnstile([FromRoute] Guid turnstileId)
        {
            var result = await _sender.Send(new GetTurnstile(turnstileId));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
        public async Task<ActionResult<List<TurnstileDto>?>> GetTurnstiles([FromQuery] GetTurnstiles query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
