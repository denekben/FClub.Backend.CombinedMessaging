using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Exceptions;
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
            var services = await _serviceRepository.GetByBranchId(command.Id)
                ?? throw new NotFoundException($"Cannot find services by branch {command.Id}");
            await _branchRepository.DeleteAsync(command.Id);
            await _repository.SaveChangesAsync();
        }
    }
}
