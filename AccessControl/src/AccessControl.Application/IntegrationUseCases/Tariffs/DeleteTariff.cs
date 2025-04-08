using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Tariffs
{
    public sealed record DeleteTariff(Guid Id) : IRequest;
}
