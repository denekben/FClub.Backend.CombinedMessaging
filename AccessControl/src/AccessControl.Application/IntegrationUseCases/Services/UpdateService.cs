using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Services
{
    public sealed record UpdateService(Guid Id, string Name) : IRequest;
}
