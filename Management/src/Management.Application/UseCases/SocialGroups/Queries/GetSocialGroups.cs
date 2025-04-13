using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.SocialGroups.Queries
{
    public sealed record GetSocialGroups(
        string? NameSearchPhrase,
        bool? SortByCreatedDate,
        int PageNumber = 1,
        int PageSize = 20
    ) : IRequest<List<SocialGroupDto>?>;
}
