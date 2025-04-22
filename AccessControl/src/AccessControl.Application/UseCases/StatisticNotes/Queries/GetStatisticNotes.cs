using AccessControl.Domain.DTOs;
using MediatR;

namespace AccessControl.Application.UseCases.StatisticNotes.Queries
{
    public sealed record GetStatisticNotes(
        Guid? BranchId,
        DateTime StartDate,
        DateTime EndDate
    ) : IRequest<List<StatisticNoteDto>?>;
}
