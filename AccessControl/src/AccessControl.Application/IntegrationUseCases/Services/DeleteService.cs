using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Services
{
    public sealed record DeleteService(Guid serviceId) : IRequest;
}
