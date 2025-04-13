using AccessControl.Application.IntegrationUseCases.Clients;
using AccessControl.Application.IntegrationUseCases.Clients.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.IntegrationControllers
{
    [ApiController]
    [Route("api/access-control/internal/clients")]
    public class ClientController : ControllerBase
    {
        private readonly ISender _sender;

        public ClientController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult> CreateClient([FromBody] CreateClient command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{clientId:guid}")]
        public async Task<ActionResult> DeleteClient([FromRoute] DeleteClient command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateClient([FromBody] UpdateClient command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPost]
        [Route("register-user")]
        public async Task<ActionResult> RegisterNewUser([FromBody] RegisterNewUser command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
