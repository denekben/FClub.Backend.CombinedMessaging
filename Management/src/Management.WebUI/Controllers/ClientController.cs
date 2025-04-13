using Management.Application.UseCases.Clients.Commands;
using Management.Application.UseCases.Clients.Queries;
using Management.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
    [Route("api/management/clients")]
    public class ClientController : ControllerBase
    {
        private readonly ISender _sender;

        public ClientController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto?>> CreateClient([FromBody] CreateClient command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{ClientId:guid}")]
        public async Task<ActionResult> DeleteClient([FromRoute] DeleteClient command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<ClientDto?>> UpdateClient([FromBody] UpdateClient command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("{clientId:guid}")]
        public async Task<ActionResult<List<ClientDto>?>> GetClient([FromRoute] Guid clientId)
        {
            var result = await _sender.Send(new GetClient(clientId));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDto>?>> GetClients([FromQuery] GetClients query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
