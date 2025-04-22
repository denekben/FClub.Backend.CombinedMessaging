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
        private readonly IServiceTariffRepository _serviceTariffRepository;
        private readonly IRepository _repository;

        public UpdateTariffHandler(ITariffRepository tariffRepository, IRepository repository,
            IHttpAccessControlClient accessControlClient, IServiceRepository serviceRepository,
            IServiceTariffRepository serviceTariffRepository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
            _accessControlClient = accessControlClient;
            _serviceRepository = serviceRepository;
            _serviceTariffRepository = serviceTariffRepository;
        }

        public async Task<TariffDto?> Handle(UpdateTariff command, CancellationToken cancellationToken)
        {
            var serviceToAddClient = new List<AccessControlServiceDto>();
            var services = new List<Service>();
            var (id, name, priceForNMonths, discountForSocialGroup, allowMultiBranches, serviceNames) = command;

            var tariff = await _tariffRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find tariff {id}");

            tariff.UpdateDetails(name, priceForNMonths, discountForSocialGroup, allowMultiBranches);

            await _serviceTariffRepository.DeleteByTariffId(id);

            foreach (var serviceName in serviceNames)
            {
                var service = await _serviceRepository.GetByNameNoTrackingAsync(serviceName);
                if (service == null)
                {
                    service = Service.Create(serviceName);
                    serviceToAddClient.Add(new Shared.IntegrationUseCases.AccessControl.DTOs.ServiceDto(service.Id, serviceName));
                    await _serviceRepository.AddAsync(service);
                }

                await _serviceTariffRepository.AddAsync(ServiceTariff.Create(service.Id, id));
                services.Add(service);
            }

            await _accessControlClient.UpdateTariff(
                new(
                    tariff.Id,
                    tariff.Name,
                    tariff.AllowMultiBranches,
                    tariff.ServiceTariffs.Select(st => new AccessControlServiceTariffDto(st.Id, st.ServiceId, st.TariffId)).ToList(),
                    serviceToAddClient
            ));

            await _repository.SaveChangesAsync();

            return tariff.AsDto(services);
        }
    }
}