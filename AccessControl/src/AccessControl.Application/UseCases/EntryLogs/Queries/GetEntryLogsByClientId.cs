using AccessControl.Domain.DTOs;
using MediatR;

namespace AccessControl.Application.UseCases.ClientLogs.Queries
{
    public sealed record GetEntryLogsByClientId : IRequest<List<EntryLogDto>?>;
}
