using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.IntegrationUseCases.Tariffs;

namespace Notifications.WebUI.IntegrationControllers
{
    [ApiController]
    [Route("api/notifications/internal/tariffs")]
    public class TariffController : ControllerBase
    {
        private readonly ISender _sender;

        public TariffController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTariff([FromBody] CreateTariff command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
