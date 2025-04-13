using AccessControl.Domain.DTOs;
using MediatR;

namespace AccessControl.Application.UseCases.UserLogs.Queries
{
    public sealed record GetUserLogs(
        Guid? UserId,
        string? TextSearchPhrase,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<UserLogDto>?>;
}
