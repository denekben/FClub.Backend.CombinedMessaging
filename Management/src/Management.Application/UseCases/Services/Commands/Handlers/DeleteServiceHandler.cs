using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Services.Commands.Handlers
{
    public sealed class DeleteServiceHandler : IRequestHandler<DeleteService>
    {
        private readonly IHttpAccessControlClient _accessControlClient;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public DeleteServiceHandler(IServiceRepository serviceRepository, IRepository repository,
            IHttpAccessControlClient accessControlClient)
        {
            _serviceRepository = serviceRepository;
            _repository = repository;
            _accessControlClient = accessControlClient;
        }

        public async Task Handle(DeleteService command, CancellationToken cancellationToken)
        {
            var service = await _serviceRepository.GetAsync(command.serviceId)
                ?? throw new NotFoundException($"Cannot find service {command.serviceId}");
            await _serviceRepository.DeleteAsync(command.serviceId);

            await _accessControlClient.DeleteService(
                new(service.Id)
            );

            await _repository.SaveChangesAsync();
        }
    }
}
