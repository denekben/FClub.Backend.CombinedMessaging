using AccessControl.Application.UseCases.ClientLogs.Queries;
using AccessControl.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.WebUI.Controllers
{
    [ApiController]
    [Authorize(Policy = "IsNotBlocked", Roles = "Manager,Admin")]
    [Route("api/access-control/entry-logs")]
    public class EntryLogController : ControllerBase
    {
        private readonly ISender _sender;

        public EntryLogController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<EntryLogDto>?>> GetEntryLogs([FromQuery] GetEntryLogs query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
