using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Commands
{
    public sealed record DeleteTurnstile(Guid TurnstileId) : IRequest;
}
