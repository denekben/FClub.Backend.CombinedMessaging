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
        private readonly IRepository _repository;

        public CreateBranchHandler(
            IBranchRepository branchRepository,
            IRepository repository)
        {
            _branchRepository = branchRepository;
            _repository = repository;
        }

        public async Task Handle(CreateBranch command, CancellationToken cancellationToken)
        {
            var (id, name, maxOccupancy, country, city, street, houseNumber, serviceBranches, services) = command;

            var branch = Branch.Create(id, name, maxOccupancy, country, city, street, houseNumber);

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