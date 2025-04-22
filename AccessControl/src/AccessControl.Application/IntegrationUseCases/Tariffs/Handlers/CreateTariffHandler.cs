using AccessControl.Domain.Entities;
using AccessControl.Domain.Entities.Pivots;
using AccessControl.Domain.Repositories;
using AccessControl.Domain.Entities;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Tariffs.Handler
{
    [SkipLogging]
    public sealed class CreateTariffHandler : IRequestHandler<CreateTariff>
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public CreateTariffHandler(ITariffRepository tariffRepository, IRepository repository,
            IServiceRepository serviceRepository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(CreateTariff command, CancellationToken cancellationToken)
        {
            var (id, name, allowMultiBranches, serviceTariffsDto, servicesDto) = command;
            var tariff = Tariff.Create(id, name, allowMultiBranches);

            var serviceTariffs = new List<ServiceTariff>();
            foreach (var serviceTariffDto in serviceTariffsDto)
            {
                serviceTariffs.Add(ServiceTariff.Create(serviceTariffDto.Id, serviceTariffDto.ServiceId, serviceTariffDto.TariffId));
            }

            var services = new List<Service>();
            foreach (var serviceDto in servicesDto)
            {
                var service = await _serviceRepository.GetAsync(serviceDto.Id);
                if (service == null)
                {
                    service = Service.Create(serviceDto.Id, serviceDto.Name);
                    await _serviceRepository.AddAsync(service);
                }
                services.Add(service);
            }

            foreach (var serviceBranch in serviceTariffs)
            {
                var service = services.First(s => s.Id == serviceBranch.ServiceId);
                serviceBranch.Service = service;
            }
            tariff.ServiceTariffs = serviceTariffs;

            await _tariffRepository.AddAsync(tariff);

            await _repository.SaveChangesAsync();
        }
    }
}