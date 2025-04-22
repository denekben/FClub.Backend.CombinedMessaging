using AccessControl.Domain.Entities.Pivots;
using AccessControl.Domain.Repositories;
using AccessControll.Domain.Entities;
using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Tariffs.Handler
{
    [SkipLogging]
    public sealed class UpdateTariffHandler : IRequestHandler<UpdateTariff>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IServiceTariffRepository _serviceTariffRepository;
        private readonly IRepository _repository;

        public UpdateTariffHandler(ITariffRepository tariffRepository, IRepository repository,
            IServiceRepository serviceRepository, IServiceTariffRepository serviceTariffRepository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
            _serviceRepository = serviceRepository;
            _serviceTariffRepository = serviceTariffRepository;
        }

        public async Task Handle(UpdateTariff command, CancellationToken cancellationToken)
        {
            var (id, name, allowMultiBranches, serviceTariffs, services) = command;

            var tariff = await _tariffRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find branch {id}");

            tariff.UpdateDetails(name, allowMultiBranches);

            await _serviceTariffRepository.DeleteByTariffId(id);

            foreach (var service in services)
            {
                await _serviceRepository.AddAsync(Service.Create(service.Id, service.Name));
            }

            foreach (var serviceTariff in serviceTariffs)
            {
                await _serviceTariffRepository.AddAsync(ServiceTariff.Create(serviceTariff.Id, serviceTariff.ServiceId, serviceTariff.TariffId));
            }

            await _repository.SaveChangesAsync();
        }
    }
}
