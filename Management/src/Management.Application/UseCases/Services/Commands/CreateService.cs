using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Services.Commands
{
    public sealed record CreateService(string Name) : IRequest<ServiceDto?>;
}
