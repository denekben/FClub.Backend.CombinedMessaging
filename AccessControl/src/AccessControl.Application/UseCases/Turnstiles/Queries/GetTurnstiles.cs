using AccessControl.Domain.DTOs;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Queries
{
    public sealed record GetTurnstiles : IRequest<List<TurnstileDto>?>;
}
