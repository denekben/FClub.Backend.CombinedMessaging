using AccessControl.Domain.Entities.Pivots;
using AccessControl.Domain.Repositories;
using AccessControll.Domain.Entities;
using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Branches.Handlers
{
    [SkipLogging]
    public sealed class UpdateBranchHandler : IRequestHandler<UpdateBranch>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IRepository _repository;

        public UpdateBranchHandler(IBranchRepository branchRepository, IRepository repository,
            IServiceRepository serviceRepository)
        {
            _branchRepository = branchRepository;
            _repository = repository;
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(UpdateBranch command, CancellationToken cancellationToken)
        {
            var (id, name, maxOccupancy, country, city, street, houseNumber, serviceBranches, services) = command;

            var branch = await _branchRepository.GetAsync(id, includes: BranchIncludes.ServicesBranches)
                ?? throw new NotFoundException($"Cannot find branch {id}");

            branch.UpdateDetails(name, maxOccupancy, country, city, street, houseNumber);

            var existingServiceIds = branch.ServiceBranches.Select(sb => sb.ServiceId).ToList();
            var newServiceIds = serviceBranches.Select(sb => sb.ServiceId).ToList();

            var serviceBranchesToRemove = branch.ServiceBranches
                .Where(sb => !newServiceIds.Contains(sb.ServiceId))
                .ToList();

            var serviceBranchesToAdd = serviceBranches
                .Where(sb => !existingServiceIds.Contains(sb.ServiceId))
                .ToList();

            foreach (var sbDto in serviceBranchesToAdd)
            {
                var service = await _serviceRepository.GetAsync(sbDto.ServiceId);
                if (service == null)
                {
                    var serviceDto = services.First(s => s.Id == sbDto.ServiceId);
                    service = Service.Create(serviceDto.Id, serviceDto.Name);
                    await _serviceRepository.AddAsync(service);
                }

                branch.ServiceBranches.Add(ServiceBranch.Create(sbDto.Id, sbDto.ServiceId, sbDto.BranchId));
            }

            foreach (var sb in serviceBranchesToRemove)
            {
                branch.ServiceBranches.Remove(sb);
            }

            await _repository.SaveChangesAsync();
        }
    }
}