using AccessControl.Domain.Repositories;
using AccessControl.Domain.Entities;
using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Logging;
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
            if (!await _serviceRepository.ExistsAsync(id))
                throw new NotFoundException($"Cannot find service {id}");
            await _serviceRepository.AddAsync(Service.Create(id, name));
            await _repository.SaveChangesAsync();
        }
    }
}
