using AccessControl.Application.IntegrationUseCases.Tariffs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.IntegrationControllers
{
    [ApiController]
    [Authorize(Policy = "ManagementAudience")]
    [Route("api/access-control/internal/tariffs")]
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

        [HttpDelete]
        [Route("{tariffId:guid}")]
        public async Task<ActionResult> DeleteTariff([FromRoute] DeleteTariff command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTariff([FromBody] UpdateTariff command)
        {
            await _sender.Send(command);
            return Ok();
        }
    }
}
