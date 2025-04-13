using AccessControl.Domain.DTOs;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Queries
{
    public sealed record GetTurnstiles(
        string? NameSearchPhrase,
        bool? IsMain,
        Guid? BranchId,
        Guid? ServiceId,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<TurnstileDto>?>;
}