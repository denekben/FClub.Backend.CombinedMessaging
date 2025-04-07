using AccessControl.Domain.DTOs;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Commands
{
    public sealed record CreateTurnstile(
        string? Name,
        bool IsMain,
        Guid BranchId,
        Guid? ServiceId
    ) : IRequest<TurnstileDto?>;
}