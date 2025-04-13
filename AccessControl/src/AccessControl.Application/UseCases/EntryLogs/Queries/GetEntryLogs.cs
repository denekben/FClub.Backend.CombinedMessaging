using AccessControl.Domain.DTOs;
using MediatR;

namespace AccessControl.Application.UseCases.ClientLogs.Queries
{
    public sealed record GetEntryLogs(
        Guid? ClientId,
        Guid? TurnstileId,
        string? ClientNameSearchPhrase,
        string? BranchNameSearchPhrase,
        string? ServiceNameSearchPhrase,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<EntryLogDto>?>;
}
