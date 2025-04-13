using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Clients.Commands
{
    public sealed record UpdateClient(
        Guid Id,
        string FirstName,
        string SecondName,
        string? Patronymic,
        string? Phone,
        string Email,
        bool IsStaff,
        bool AllowEntry,
        bool AllowNotifications,
        Guid? MembershipId,
        Guid? SocialGroupId
    ) : IRequest<ClientDto?>;
}
