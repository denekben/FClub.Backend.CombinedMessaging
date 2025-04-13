using AccessControl.Domain.Enums;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Commands
{
    public sealed record GoThrough(Guid ClientId, Guid TurnstileId, EntryType entryType) : IRequest;
}
