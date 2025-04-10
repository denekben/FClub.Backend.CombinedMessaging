using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Services.Commands.Handlers
{
    public sealed class UpdateServiceHandler : IRequestHandler<UpdateService, ServiceDto?>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public UpdateServiceHandler(IServiceRepository serviceRepository, IRepository repository,
            IHttpAccessControlClient accessControlClient)
        {
            _serviceRepository = serviceRepository;
            _repository = repository;
            _accessControlClient = accessControlClient;
        }

        public async Task<ServiceDto?> Handle(UpdateService command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetAsync(command.Id)
                ?? throw new NotFoundException($"Cannot find service {command.Id}");

            service.UpdateDetails(command.Name);

            await _accessControlClient.UpdateService(
                new(service.Id, service.Name)
            );

            await _repository.SaveChangesAsync();

            return service.AsDto();
        }
    }
}
