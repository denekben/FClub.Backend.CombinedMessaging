using Management.Application.UseCases.AppUsers.Commands;
using Management.Application.UseCases.AppUsers.Queries;
using Management.Application.UseCases.UserLogs.Queries;
using Management.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebUI.Controllers
{
    [ApiController]
    [Route("api/management/users")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut]
        [Route("assign-to-role")]
        [Authorize(Policy = "IsNotBlocked", Roles = "Admin")]
        public async Task<ActionResult> AssignUserToRole([FromBody] AssignUserToRole command)
        {
            await _sender.Send(command);
            return Ok();
        }

        [HttpGet]
        [Route("access-token")]
        public async Task<ActionResult<string?>> RefreshExpiredToken([FromBody] RefreshExpiredToken command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        [Authorize(Policy = "IsNotBlocked", Roles = "Admin")]
        public async Task<ActionResult<TokensDto?>> RegisterNewUser([FromBody] RegisterNewUser command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }

        [HttpPut]
        [Route("sign-in")]
        public async Task<ActionResult<TokensDto?>> SignIn([FromBody] SignIn command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }

        [HttpGet]
        [Route("current")]
        [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
        public async Task<ActionResult<UserDto?>> GetCurrentUser([FromQuery] GetCurrentUser query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("{userId:guid}")]
        [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
        public async Task<ActionResult<UserDto?>> GetUser([FromRoute] Guid userId)
        {
            var result = await _sender.Send(new GetUser(userId));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
        public async Task<ActionResult<List<UserDto>?>> GetUsers([FromQuery] GetUsers query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("current/logs")]
        [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
        public async Task<ActionResult<List<UserLogDto>?>> GetCurrentUserLogs([FromQuery] GetCurrentUserLogs query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Route("logs")]
        [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
        public async Task<ActionResult<List<UserLogDto>?>> GetUserLogs([FromQuery] GetLogs query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut]
        [Route("{userId:guid}/block")]
        [Authorize(Policy = "IsNotBlocked", Roles = "Admin")]
        public async Task<ActionResult> BlockUser([FromRoute] Guid userId)
        {
            await _sender.Send(new BlockUser(userId));
            return Ok();
        }

        [HttpPut]
        [Route("{userId:guid}/unblock")]
        [Authorize(Policy = "IsNotBlocked", Roles = "Admin")]
        public async Task<ActionResult> UnblockUser([FromRoute] Guid userId)
        {
            await _sender.Send(new UnblockUser(userId));
            return Ok();
        }
    }
}
