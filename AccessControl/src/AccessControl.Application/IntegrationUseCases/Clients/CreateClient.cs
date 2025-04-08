using AccessControl.Domain.Entities;
using MediatR;

namespace AccessControl.Application.UseCases.Clients.Commands
{
    public sealed record CreateClient(
        Guid Id,
        string FirstName,
        string SecondName,
        string? Patronymic,
        string? Phone,
        string Email,
        bool AllowEntry,
        Membership? Membership
    ) : IRequest;
}
