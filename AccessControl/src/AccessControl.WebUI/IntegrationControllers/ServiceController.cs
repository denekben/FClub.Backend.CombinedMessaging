using AccessControl.Application.IntegrationUseCases.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.IntegrationControllers
{
    [ApiController]
    [Route("api/access-control/internal/services")]
    public class ServiceController : ControllerBase
    {
        private readonly ISender _sender;

        public ServiceController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult> CreateService([FromBody] CreateService command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpDelete]
        [Route("{serviceId:guid}")]
        public async Task<ActionResult> DeleteService([FromRoute] DeleteService command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateService([FromBody] UpdateService command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
