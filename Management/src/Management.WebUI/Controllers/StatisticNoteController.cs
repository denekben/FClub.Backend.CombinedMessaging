using Management.Application.UseCases.StatisticNotes.Queries;
using Management.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebUI.Controllers
{
    [ApiController]
    [Route("api/management/statistic-notes")]
    public class StatisticNoteController : ControllerBase
    {
        private readonly ISender _sender;

        public StatisticNoteController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<StatisticNoteDto>?>> GetSocialGroups([FromQuery] GetStatisticNotes query)
        {
            var result = await _sender.Send(query);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
