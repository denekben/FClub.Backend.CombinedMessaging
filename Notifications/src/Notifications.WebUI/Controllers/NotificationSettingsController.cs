using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.UseCases.NotificationSettings.Commands;
using Notifications.Application.UseCases.NotificationSettings.Queries;
using Notifications.Domain.DTOs;

namespace Notifications.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = "IsNotBlocked")]
    public class NotificationSettingsController : ControllerBase
    {
        private readonly ISender _sender;

        public NotificationSettingsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<NotificationSettingsDto?>> UpdateNotificationSettings([FromBody] UpdateNotificationSettings command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<ActionResult<NotificationSettingsDto?>> GetNotificationSettings([FromBody] GetNotificationSettings command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
