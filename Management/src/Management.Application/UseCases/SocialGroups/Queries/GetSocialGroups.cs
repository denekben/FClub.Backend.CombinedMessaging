using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.SocialGroups.Queries
{
    public sealed record GetSocialGroups : IRequest<List<SocialGroupDto>?>;
}
