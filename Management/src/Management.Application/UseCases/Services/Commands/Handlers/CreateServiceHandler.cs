using Management.Application.Services;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Services.Commands.Handlers
{
    public sealed class CreateServiceHandler : IRequestHandler<CreateService, ServiceDto?>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public CreateServiceHandler(IServiceRepository serviceRepository, IRepository repository,
            IHttpAccessControlClient accessControlClient)
        {
            _serviceRepository = serviceRepository;
            _repository = repository;
            _accessControlClient = accessControlClient;
        }

        public async Task<ServiceDto?> Handle(CreateService command, CancellationToken cancellationToken)
        {
            var service = Service.Create(command.Name);
            await _serviceRepository.AddAsync(service);

            await _accessControlClient.CreateService(
                new(service.Id, service.Name)
            );

            await _repository.SaveChangesAsync();

            return service.AsDto();
        }
    }
}
