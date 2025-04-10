using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Tariffs.Commands.Handlers
{
    public sealed class DeleteTariffHandler : IRequestHandler<DeleteTariff>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IServiceRepository _serviceRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IRepository _repository;

        public DeleteTariffHandler(ITariffRepository tariffRepository, IRepository repository,
            IHttpAccessControlClient accessControlClient, IServiceRepository serviceRepository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
            _accessControlClient = accessControlClient;
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(DeleteTariff command, CancellationToken cancellationToken)
        {
            var tariff = await _tariffRepository.GetAsync(command.Id)
                ?? throw new NotFoundException($"Cannot find tariff {command.Id}");

            var serviceIds = tariff.ServiceTariffs.Select(sb => sb.ServiceId).ToList();

            await _tariffRepository.DeleteAsync(command.Id);

            await _accessControlClient.DeleteTariff(
                new(tariff.Id)
            );

            await _repository.SaveChangesAsync();
        }
    }
}
