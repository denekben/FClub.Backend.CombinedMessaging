using Management.Application.UseCases.SocialGroups.Commands;
using Management.Application.UseCases.SocialGroups.Queries;
using Management.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebUI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/management/social-groups")]
    public class SocialGroupController : ControllerBase
    {
        private readonly ISender _sender;

        public SocialGroupController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<SocialGroupDto?>> CreateSocialGroup([FromBody] CreateSocialGroup command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{socialGroupId:guid}")]
        public async Task<ActionResult> DeleteSocialGroup([FromRoute] DeleteSocialGroup command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<SocialGroupDto?>> UpdateSocialGroup([FromBody] UpdateSocialGroup command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<SocialGroupDto>?>> GetSocialGroups([FromQuery] GetSocialGroups query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
