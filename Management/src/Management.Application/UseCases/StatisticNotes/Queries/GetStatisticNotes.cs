using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.StatisticNotes.Queries
{
    public sealed record GetStatisticNotes : IRequest<List<StatisticNoteDto>?>;
}
