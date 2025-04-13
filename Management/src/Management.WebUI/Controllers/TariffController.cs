using Management.Application.UseCases.Tariffs.Commands;
using Management.Application.UseCases.Tariffs.Queries;
using Management.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = "IsNotBlocked")]
    [Route("api/management/tariffs")]
    public class TariffController : ControllerBase
    {
        private readonly ISender _sender;

        public TariffController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TariffDto?>> CreateTariff([FromBody] CreateTariff command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{tariffId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTariff([FromRoute] DeleteTariff command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TariffDto?>> UpdateTariff([FromBody] UpdateTariff command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<List<TariffDto>?>> GetTariffs([FromQuery] GetTariffs query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
