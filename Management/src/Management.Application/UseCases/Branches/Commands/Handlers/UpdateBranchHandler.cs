using FClub.Backend.Common.Exceptions;
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
    public sealed class UpdateBranchHandler : IRequestHandler<UpdateBranch, BranchDto?>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IBranchRepository _branchRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public UpdateBranchHandler(
            IBranchRepository branchRepository, IServiceRepository serviceRepository, IRepository repository,
            IHttpAccessControlClient accessControlClient)
        {
            _branchRepository = branchRepository;
            _serviceRepository = serviceRepository;
            _repository = repository;
            _accessControlClient = accessControlClient;
        }

        public async Task<BranchDto?> Handle(UpdateBranch command, CancellationToken cancellationToken)
        {
            var (branchId, name, maxOccupancy, country, city, street, houseNumber, serviceNames) = command;

            var branch = await _branchRepository.GetAsync(branchId, BranchIncludes.Services)
                ?? throw new NotFoundException($"Cannot find {branchId}");

            branch.UpdateDetails(name, maxOccupancy, country, city, street, houseNumber);

            var serviceNamesToDelete = branch.ServiceBranches.Select(sb => sb.Service.Name).Except(serviceNames).ToList();
            var serviceNamesToAdd = serviceNames.Except(branch.ServiceBranches.Select(sb => sb.Service.Name)).ToList();

            foreach (var serviceName in serviceNamesToAdd)
            {
                var service = await _serviceRepository.GetByNameAsync(serviceName);
                if (service == null)
                {
                    service = Service.Create(serviceName);
                    await _serviceRepository.AddAsync(service);
                }
                branch.ServiceBranches.Add(ServiceBranch.Create(service.Id, branch.Id));
            }

            foreach (var serviceName in serviceNamesToDelete)
            {
                var service = await _serviceRepository.GetByNameAsync(serviceName);
                if (service != null)
                {
                    var serviceBranchToRemove = branch.ServiceBranches.FirstOrDefault(sb => sb.ServiceId == service.Id);
                    if (serviceBranchToRemove != null)
                    {
                        branch.ServiceBranches.Remove(serviceBranchToRemove);
                    }
                }
            }

            var services = branch.ServiceBranches.Select(sb => sb.Service);
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
                    services.Select(s => new AccessControlServiceDto(s.Id, s.Name)).ToList())
            );

            await _repository.SaveChangesAsync();

            return branch.AsDto(services.ToList());
        }
    }
}
