using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Services
{
    public sealed record CreateService(Guid Id, string Name) : IRequest;
}
