using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.UserLogs.Queries
{
    public sealed record GetLogs(
        Guid? UserId,
        string? TextSearchPhrase,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<UserLogDto>?>;
}