using AccessControl.Domain.Repositories;
using AccessControl.Shared.Logging;
using FClub.Backend.Common.Exceptions;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Services.Handlers
{
    [SkipLogging]
    public sealed class UpdateServiceHandler : IRequestHandler<UpdateService>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public UpdateServiceHandler(IServiceRepository serviceRepository, IRepository repository)
        {
            _serviceRepository = serviceRepository;
            _repository = repository;
        }

        public async Task Handle(UpdateService command, CancellationToken cancellationToken)
        {
            var (id, name) = command;
            var service = await _serviceRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find service {id}");
            await _repository.SaveChangesAsync();
        }
    }
}
