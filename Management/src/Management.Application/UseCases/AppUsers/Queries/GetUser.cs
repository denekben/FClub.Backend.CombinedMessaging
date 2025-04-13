using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.AppUsers.Queries
{
    public sealed record GetUser(Guid UserId) : IRequest<UserDto?>;
}
