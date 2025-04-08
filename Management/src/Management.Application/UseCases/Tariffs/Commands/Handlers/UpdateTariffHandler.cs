using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Entities.Pivots;
using Management.Domain.Repositories;
using MediatR;
using AccessControlServiceDto = Management.Shared.IntegrationUseCases.AccessControl.DTOs.ServiceDto;
using AccessControlServiceTariffDto = Management.Shared.IntegrationUseCases.AccessControl.DTOs.ServiceTariffDto;

namespace Management.Application.UseCases.Tariffs.Commands.Handlers
{
    public sealed class UpdateTariffHandler : IRequestHandler<UpdateTariff, TariffDto?>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IServiceRepository _serviceRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IRepository _repository;

        public UpdateTariffHandler(ITariffRepository tariffRepository, IRepository repository,
            IHttpAccessControlClient accessControlClient, IServiceRepository serviceRepository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
            _accessControlClient = accessControlClient;
            _serviceRepository = serviceRepository;
        }

        public async Task<TariffDto?> Handle(UpdateTariff command, CancellationToken cancellationToken)
        {
            var (id, name, priceForNMonths, discountForSocialGroup, allowMultiBranches, serviceNames) = command;

            var tariff = await _tariffRepository.GetAsync(id, TariffIncludes.ServiceTariffs | TariffIncludes.Services)
                ?? throw new NotFoundException($"Cannot find tariff {command.Id}");

            tariff.UpdateDetails(name, priceForNMonths, discountForSocialGroup, allowMultiBranches);

            var serviceNamesToDelete = tariff.ServiceTariffs.Select(sb => sb.Service.Name).Except(serviceNames).ToList();
            var serviceNamesToAdd = serviceNames.Except(tariff.ServiceTariffs.Select(sb => sb.Service.Name)).ToList();

            var services = tariff.ServiceTariffs.Select(st => st.Service).ToList() ?? [];
            foreach (var serviceName in serviceNamesToAdd)
            {
                var service = await _serviceRepository.GetByNameAsync(serviceName);
                if (service == null)
                {
                    service = Service.Create(serviceName);
                    await _serviceRepository.AddAsync(service);
                }
                services.Add(service);
                tariff.ServiceTariffs.Add(ServiceTariff.Create(service.Id, tariff.Id));
            }

            await _serviceRepository.DeleteOneTariffAndZeroBranchesServicesByNameAsync(serviceNamesToDelete, tariff.Id);
            foreach (var serviceName in serviceNamesToDelete)
            {
                var service = await _serviceRepository.GetByNameAsync(serviceName);
                if (service != null)
                {
                    var serviceTariffToRemove = tariff.ServiceTariffs.FirstOrDefault(sb => sb.ServiceId == service.Id);
                    if (serviceTariffToRemove != null)
                    {
                        services.Remove(service);
                        tariff.ServiceTariffs.Remove(serviceTariffToRemove);
                    }
                }
            }

            await _tariffRepository.UpdateAsync(tariff);

            await _accessControlClient.UpdateTariff(
                new(
                    tariff.Id,
                    tariff.Name,
                    tariff.AllowMultiBranches,
                    tariff.ServiceTariffs.Select(st => new AccessControlServiceTariffDto(st.Id, st.ServiceId, st.TariffId)).ToList(),
                    services.Select(s => new AccessControlServiceDto(s.Id, s.Name)).ToList())
            );

            await _repository.SaveChangesAsync();

            return tariff.AsDto(services);
        }
    }
}
