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
    public sealed class CreateTariffHandler : IRequestHandler<CreateTariff, TariffWithGroupsDto?>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IHttpNotificationsClient _notificationsClient;
        private readonly ISocialGroupRepository _socialGroupRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public CreateTariffHandler(ITariffRepository tariffRepository, IRepository repository,
            IHttpAccessControlClient accessControlClient, IServiceRepository serviceRepository,
            IHttpNotificationsClient notificationsClient, ISocialGroupRepository socialGroupRepository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
            _accessControlClient = accessControlClient;
            _serviceRepository = serviceRepository;
            _notificationsClient = notificationsClient;
            _socialGroupRepository = socialGroupRepository;
        }

        public async Task<TariffWithGroupsDto?> Handle(CreateTariff command, CancellationToken cancellationToken)
        {
            var (sendNotification, name, priceForNMonths, discountForSocialGroup, allowMultiBranches, serviceNames) = command;

            var discs = new Dictionary<SocialGroup, int>();
            foreach (var disc in (discountForSocialGroup ?? []))
            {
                var group = await _socialGroupRepository.GetAsync(disc.Key) ??
                    throw new NotFoundException($"Cannot find social group {disc.Key}");
                discs.Add(group, disc.Value);
            }

            var tariff = Tariff.Create(name, priceForNMonths, discountForSocialGroup, allowMultiBranches);

            var services = new List<Service>();
            foreach (var serviceName in serviceNames)
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

            await _tariffRepository.AddAsync(tariff);

            await _accessControlClient.CreateTariff(
                new(
                    tariff.Id,
                    tariff.Name,
                    tariff.AllowMultiBranches,
                    tariff.ServiceTariffs.Select(st => new AccessControlServiceTariffDto(st.Id, st.ServiceId, st.TariffId)).ToList(),
                    services.Select(s => new AccessControlServiceDto(s.Id, s.Name)).ToList())
            );

            if (sendNotification)
            {
                await _notificationsClient.CreateTariff(
                    new(tariff.Name, tariff.PriceForNMonths, tariff.DiscountForSocialGroup, tariff.AllowMultiBranches, serviceNames)
                );
            }

            await _repository.SaveChangesAsync();

            return tariff.AsDto(services, discs);
        }
    }
}
