using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Branches.Handlers
{
    [SkipLogging]
    public sealed class DeleteBranchHandler : IRequestHandler<DeleteBranch>
    {
        private readonly IRepository _repository;
        private readonly IBranchRepository _branchRepository;
        private readonly IServiceRepository _serviceRepository;

        public DeleteBranchHandler(IRepository repository, IBranchRepository branchRepository,
            IServiceRepository serviceRepository)
        {
            _repository = repository;
            _branchRepository = branchRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task Handle(DeleteBranch command, CancellationToken cancellationToken)
        {
            Console.WriteLine(command.branchId);
            await _branchRepository.DeleteAsync(command.branchId);
            await _repository.SaveChangesAsync();
        }
    }
}
