using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Entities.Pivots;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class CreateBranchHandler : IRequestHandler<CreateBranch, BranchDto?>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IRepository _repository;

        public CreateBranchHandler(
            IServiceRepository serviceRepository, IBranchRepository branchRepository, IRepository repository)
        {
            _serviceRepository = serviceRepository;
            _branchRepository = branchRepository;
            _repository = repository;
        }

        public async Task<BranchDto?> Handle(CreateBranch command, CancellationToken cancellationToken)
        {
            var (name, maxOccupancy, country, city, street, houseNumber, serviceNames) = command;

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
            await _repository.SaveChangesAsync();

            return branch.AsDto(services);
        }
    }
}