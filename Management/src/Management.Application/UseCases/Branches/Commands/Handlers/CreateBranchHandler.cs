using Management.Application.Services;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Entities.Pivots;
using Management.Domain.Repositories;
using MediatR;
using AccessControlServiceBranchDto = Management.Shared.IntegrationUseCases.AccessControl.DTOs.ServiceBranchDto;
using AccessControlServiceDto = Management.Shared.IntegrationUseCases.AccessControl.DTOs.ServiceDto;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class CreateBranchHandler : IRequestHandler<CreateBranch, BranchDto?>
    {
        private readonly IHttpNotificationsClient _notificationClient;
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IServiceRepository _serviceRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IRepository _repository;

        public CreateBranchHandler(
            IServiceRepository serviceRepository, IBranchRepository branchRepository,
            IRepository repository, IHttpAccessControlClient accessControlClient,
            IHttpNotificationsClient notificationClient)
        {
            _serviceRepository = serviceRepository;
            _branchRepository = branchRepository;
            _repository = repository;
            _accessControlClient = accessControlClient;
            _notificationClient = notificationClient;
        }

        public async Task<BranchDto?> Handle(CreateBranch command, CancellationToken cancellationToken)
        {
            var (sendNotification, name, maxOccupancy, country, city, street, houseNumber, serviceNames) = command;

            var branch = Branch.Create(name, maxOccupancy, country, city, street, houseNumber);

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
                branch.ServiceBranches.Add(ServiceBranch.Create(service.Id, branch.Id));
            }

            await _branchRepository.AddAsync(branch);

            await _accessControlClient.CreateBranch(
                new(
                    branch.Id,
                    branch.Name,
                    branch.MaxOccupancy,
                    branch.Address.Country,
                    branch.Address.City,
                    branch.Address.Street,
                    branch.Address.HouseNumber,
                    branch.ServiceBranches.Select(sb => new AccessControlServiceBranchDto(sb.Id, sb.ServiceId, sb.BranchId)).ToList(),
                    services.Select(s => new AccessControlServiceDto(s.Id, s.Name)).ToList())
            );

            if (sendNotification)
            {
                await _notificationClient.CreateBranch(
                    new(
                    branch.Name,
                    branch.Address.Country,
                    branch.Address.City,
                    branch.Address.Street,
                    branch.Address.HouseNumber,
                    serviceNames)
                );
            }


            await _repository.SaveChangesAsync();

            return branch.AsDto(services);
        }
    }
}