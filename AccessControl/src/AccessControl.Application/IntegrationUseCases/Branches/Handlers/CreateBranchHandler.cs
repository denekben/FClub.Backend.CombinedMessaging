using AccessControl.Domain.Entities.Pivots;
using AccessControl.Domain.Repositories;
using AccessControll.Domain.Entities;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Branches.Handlers
{
    [SkipLogging]
    public sealed class CreateBranchHandler : IRequestHandler<CreateBranch>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public CreateBranchHandler(
            IBranchRepository branchRepository,
            IRepository repository,
            IServiceRepository serviceRepository)
        {
            _branchRepository = branchRepository;
            _repository = repository;
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(CreateBranch command, CancellationToken cancellationToken)
        {
            var (id, name, maxOccupancy, country, city, street, houseNumber, serviceBranchesDto, servicesDto) = command;

            var branch = Branch.Create(id, name, maxOccupancy, country, city, street, houseNumber);

            var serviceBranches = new List<ServiceBranch>();
            foreach (var serviceBranchDto in serviceBranchesDto)
            {
                serviceBranches.Add(ServiceBranch.Create(serviceBranchDto.Id, serviceBranchDto.ServiceId, serviceBranchDto.BranchId));
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

            foreach (var serviceBranch in serviceBranches)
            {
                var service = services.First(s => s.Id == serviceBranch.ServiceId);
                serviceBranch.Service = service;
            }
            branch.ServiceBranches = serviceBranches;

            await _branchRepository.AddAsync(branch);

            await _repository.SaveChangesAsync();
        }
    }
}