using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Services.Handlers
{
    [SkipLogging]
    public sealed class DeleteServiceHandler : IRequestHandler<DeleteService>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public DeleteServiceHandler(IServiceRepository serviceRepository, IRepository repository)
        {
            _serviceRepository = serviceRepository;
            _repository = repository;
        }

        public async Task Handle(DeleteService command, CancellationToken cancellationToken)
        {
            await _serviceRepository.DeleteAsync(command.serviceId);
            await _repository.SaveChangesAsync();
        }
    }
}
