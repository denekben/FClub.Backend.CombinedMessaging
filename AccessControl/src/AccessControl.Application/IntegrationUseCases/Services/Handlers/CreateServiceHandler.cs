using AccessControl.Domain.Repositories;
using AccessControll.Domain.Entities;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Services.Handlers
{
    [SkipLogging]
    public sealed class CreateServiceHandler : IRequestHandler<CreateService>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public CreateServiceHandler(IServiceRepository serviceRepository, IRepository repository)
        {
            _serviceRepository = serviceRepository;
            _repository = repository;
        }

        public async Task Handle(CreateService command, CancellationToken cancellationToken)
        {
            var (id, name) = command;
            var service = Service.Create(id, name);
            await _serviceRepository.AddAsync(service);
            await _repository.SaveChangesAsync();
        }
    }
}
