using Logging.Domain.DTOs;
using MediatR;

namespace Logging.Application.UseCases.UserLogs
{
    public sealed record GetCurrentUserLogs(
        string? TextSearchPhrase,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<UserLogDto>?>;
}