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
        private readonly IRepository _repository;

        public UpdateTariffHandler(ITariffRepository tariffRepository, IRepository repository,
            IServiceRepository serviceRepository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(UpdateTariff command, CancellationToken cancellationToken)
        {
            var (id, name, allowMultiBranches, serviceTariffs, services) = command;

            var tariff = await _tariffRepository.GetAsync(id, TariffIncludes.ServicesTariffs)
                ?? throw new NotFoundException($"Cannot find branch {id}");

            tariff.UpdateDetails(name, allowMultiBranches);

            var existingServiceIds = tariff.ServiceTariffs.Select(sb => sb.ServiceId).ToList();
            var newServiceIds = serviceTariffs.Select(sb => sb.ServiceId).ToList();

            var serviceTariffsToRemove = tariff.ServiceTariffs
                .Where(sb => !newServiceIds.Contains(sb.ServiceId))
                .ToList();

            var serviceTariffsToAdd = serviceTariffs
                .Where(sb => !existingServiceIds.Contains(sb.ServiceId))
                .ToList();

            foreach (var stDto in serviceTariffsToAdd)
            {
                var service = await _serviceRepository.GetAsync(stDto.ServiceId);
                if (service == null)
                {
                    var serviceDto = services.First(s => s.Id == stDto.ServiceId);
                    service = Service.Create(serviceDto.Id, serviceDto.Name);
                    await _serviceRepository.AddAsync(service);
                }

                tariff.ServiceTariffs.Add(ServiceTariff.Create(stDto.Id, stDto.ServiceId, stDto.TariffId));
            }

            foreach (var sb in serviceTariffsToRemove)
            {
                tariff.ServiceTariffs.Remove(sb);
            }

            await _repository.SaveChangesAsync();
        }
    }
}
