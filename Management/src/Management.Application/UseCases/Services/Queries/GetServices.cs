using Management.Domain.DTOs;
using MediatR;

namespace Management.Application.UseCases.Services.Queries
{
    public sealed record GetServices : IRequest<ServiceDto?>;
}
