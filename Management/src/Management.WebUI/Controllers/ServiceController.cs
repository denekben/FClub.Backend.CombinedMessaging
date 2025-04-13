using Management.Application.UseCases.Services.Commands;
using Management.Application.UseCases.Services.Queries;
using Management.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebUI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/management/services")]
    public class ServiceController : ControllerBase
    {
        private readonly ISender _sender;

        public ServiceController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDto?>> CreateService([FromBody] CreateService command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{serviceId:guid}")]
        public async Task<ActionResult> DeleteService([FromRoute] DeleteService command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<ServiceDto?>> UpdateService([FromBody] UpdateService command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceDto>?>> GetServices([FromQuery] GetServices query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
