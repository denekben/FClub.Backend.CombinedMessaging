using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Clients.Queries
{
    public sealed record GetClients(
        string? FullNameSearchPhrase,
        string? PhoneSearchPhrase,
        string? EmailSearchPhrase,
        bool? IsStaff,
        bool? AllowedToEntry,
        bool? AllowedNotifications,
        Guid? SocialGroupId,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<ClientDto>?>;
}
