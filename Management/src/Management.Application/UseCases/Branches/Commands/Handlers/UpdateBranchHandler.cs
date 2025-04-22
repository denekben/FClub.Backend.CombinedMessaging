using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Entities.Pivots;
using Management.Domain.Repositories;
using MediatR;
using AccessControlServiceBranchDto = Management.Shared.IntegrationUseCases.AccessControl.DTOs.ServiceBranchDto;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class UpdateBranchHandler : IRequestHandler<UpdateBranch, BranchDto?>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IBranchRepository _branchRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IServiceBranchRepository _serviceBranchRepository;
        private readonly IRepository _repository;

        public UpdateBranchHandler(
            IBranchRepository branchRepository, IServiceRepository serviceRepository, IRepository repository,
            IHttpAccessControlClient accessControlClient, IServiceBranchRepository serviceBranchRepository)
        {
            _branchRepository = branchRepository;
            _serviceRepository = serviceRepository;
            _repository = repository;
            _accessControlClient = accessControlClient;
            _serviceBranchRepository = serviceBranchRepository;
        }

        public async Task<BranchDto?> Handle(UpdateBranch command, CancellationToken cancellationToken)
        {
            var serviceToAddClient = new List<Shared.IntegrationUseCases.AccessControl.DTOs.ServiceDto>();
            var services = new List<Service>();
            var (branchId, name, maxOccupancy, country, city, street, houseNumber, serviceNames) = command;

            var branch = await _branchRepository.GetAsync(branchId)
                ?? throw new NotFoundException($"Cannot find branch {branchId}");

            branch.UpdateDetails(name, maxOccupancy, country, city, street, houseNumber);

            await _serviceBranchRepository.DeleteByBranchId(branchId);

            foreach (var serviceName in serviceNames)
            {
                var service = await _serviceRepository.GetByNameNoTrackingAsync(serviceName);
                if (service == null)
                {
                    service = Service.Create(serviceName);
                    serviceToAddClient.Add(new Shared.IntegrationUseCases.AccessControl.DTOs.ServiceDto(service.Id, serviceName));
                    await _serviceRepository.AddAsync(service);
                }
                await _serviceBranchRepository.AddAsync(ServiceBranch.Create(service.Id, branchId));
                services.Add(service);
            }

            await _accessControlClient.UpdateBranch(
                new(
                    branch.Id,
                    branch.Name,
                    branch.MaxOccupancy,
                    branch.Address.Country,
                    branch.Address.City,
                    branch.Address.Street,
                    branch.Address.HouseNumber,
                    branch.ServiceBranches.Select(sb => new AccessControlServiceBranchDto(sb.Id, sb.ServiceId, sb.BranchId)).ToList(),
                    serviceToAddClient
            ));

            await _repository.SaveChangesAsync();

            return branch.AsDto(services);
        }
    }
}