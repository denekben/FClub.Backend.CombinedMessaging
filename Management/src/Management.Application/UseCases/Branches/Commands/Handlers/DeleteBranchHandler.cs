using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Branches.Commands.Handlers
{
    public sealed class DeleteBranchHandler : IRequestHandler<DeleteBranch>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IBranchRepository _branchRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public DeleteBranchHandler(
            IBranchRepository branchRepository, IRepository repository,
            IServiceRepository serviceRepository, IHttpAccessControlClient accessControlClient)
        {
            _branchRepository = branchRepository;
            _repository = repository;
            _serviceRepository = serviceRepository;
            _accessControlClient = accessControlClient;
        }

        public async Task Handle(DeleteBranch command, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetAsync(command.Id, BranchIncludes.ServiceBranches)
                ?? throw new BadRequestException($"Cannot find branch {command.Id}");

            var serviceIds = branch.ServiceBranches.Select(sb => sb.ServiceId).ToList();

            await _branchRepository.DeleteAsync(branch.Id);

            await _accessControlClient.DeleteBranch(
                new(branch.Id)
            );

            await _repository.SaveChangesAsync();
        }
    }
}
