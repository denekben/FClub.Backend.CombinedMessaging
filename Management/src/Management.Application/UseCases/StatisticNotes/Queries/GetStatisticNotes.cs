using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.StatisticNotes.Queries
{
    public sealed record GetStatisticNotes(
        Guid? BranchId,
        DateTime StartDate,
        DateTime EndDate
    ) : IRequest<List<StatisticNoteDto>?>;
}
