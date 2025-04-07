using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.AppUsers.Queries
{
    public sealed record GetUsers : IRequest<UserDto?>;
}
