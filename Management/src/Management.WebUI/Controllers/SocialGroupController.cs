using Management.Application.UseCases.SocialGroups.Commands;
using Management.Application.UseCases.SocialGroups.Queries;
using Management.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = "IsNotBlocked")]
    [Route("api/management/social-groups")]
    public class SocialGroupController : ControllerBase
    {
        private readonly ISender _sender;

        public SocialGroupController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SocialGroupDto?>> CreateSocialGroup([FromBody] CreateSocialGroup command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpDelete]
        [Route("{socialGroupId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSocialGroup([FromRoute] DeleteSocialGroup command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SocialGroupDto?>> UpdateSocialGroup([FromBody] UpdateSocialGroup command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<List<SocialGroupDto>?>> GetSocialGroups([FromQuery] GetSocialGroups query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
