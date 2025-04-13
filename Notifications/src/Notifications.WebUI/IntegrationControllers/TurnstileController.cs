using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.IntegrationUseCases.Turnstiles.Commands;

namespace Notifications.WebUI.IntegrationControllers
{
    [ApiController]
    [Route("api/notifications/internal/turnstiles")]
    public class TurnstileController : ControllerBase
    {
        private readonly ISender _sender;

        public TurnstileController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut]
        [Route("go-through")]
        public async Task<ActionResult> GoThrough([FromBody] GoThrough command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
