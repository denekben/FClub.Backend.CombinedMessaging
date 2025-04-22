using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Tariffs
{
    public sealed record DeleteTariff(Guid tariffId) : IRequest;
}
