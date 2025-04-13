using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.UseCases.NotificationSettings.Commands;
using Notifications.Application.UseCases.NotificationSettings.Queries;
using Notifications.Domain.DTOs;

namespace Notifications.WebUI.Controllers
{
    [ApiController]
    [Route("api/notifications/notification-settings")]
    public class NotificationSettingsController : ControllerBase
    {
        private readonly ISender _sender;

        public NotificationSettingsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut]
        public async Task<ActionResult<NotificationSettingsDto?>> UpdateNotificationSettings([FromBody] UpdateNotificationSettings command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<NotificationSettingsDto?>> GetNotificationSettings([FromBody] GetNotificationSettings command)
        {
            var result = await _sender.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
