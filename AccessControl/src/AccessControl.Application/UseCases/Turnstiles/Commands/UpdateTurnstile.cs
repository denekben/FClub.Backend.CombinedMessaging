using AccessControl.Domain.DTOs;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Commands
{
    public sealed record UpdateTurnstile(
        Guid TurnstileId,
        string? Name,
        bool IsMain,
        Guid BranchId,
        Guid? ServiceId
    ) : IRequest<TurnstileDto?>;
}
