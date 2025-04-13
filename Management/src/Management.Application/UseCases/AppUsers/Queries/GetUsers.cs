using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.AppUsers.Queries
{
    public sealed record GetUsers(
        string? FullNameSearchPhrase,
        string? PhoneSearchPhrase,
        string? EmailSearchPhrase,
        bool? IsBlocked,
        bool? AllowedToEntry,
        Guid? RoleId,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<UserDto>?>;
}