using AccessControl.Domain.Entities.Pivots;
using AccessControl.Domain.Repositories;
using AccessControl.Domain.Entities;
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
        private readonly IServiceBranchRepository _serviceBranchRepository;
        private readonly IRepository _repository;

        public UpdateBranchHandler(IBranchRepository branchRepository, IRepository repository,
            IServiceRepository serviceRepository, IServiceBranchRepository serviceBranchRepository)
        {
            _branchRepository = branchRepository;
            _repository = repository;
            _serviceRepository = serviceRepository;
            _serviceBranchRepository = serviceBranchRepository;
        }

        public async Task Handle(UpdateBranch command, CancellationToken cancellationToken)
        {
            var (id, name, maxOccupancy, country, city, street, houseNumber, serviceBranches, services) = command;

            var branch = await _branchRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find branch {id}");

            branch.UpdateDetails(name, maxOccupancy, country, city, street, houseNumber);

            await _serviceBranchRepository.DeleteByBranchId(id);

            foreach (var service in services)
            {
                await _serviceRepository.AddAsync(Service.Create(service.Id, service.Name));
            }

            foreach (var serviceBranch in serviceBranches)
            {
                await _serviceBranchRepository.AddAsync(ServiceBranch.Create(serviceBranch.Id, serviceBranch.ServiceId, serviceBranch.BranchId));
            }

            await _repository.SaveChangesAsync();
        }
    }
}