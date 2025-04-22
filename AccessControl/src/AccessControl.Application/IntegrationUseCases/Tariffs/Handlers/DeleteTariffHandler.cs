using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Tariffs.Handler
{
    [SkipLogging]
    public sealed class DeleteTariffHandler : IRequestHandler<DeleteTariff>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IRepository _repository;

        public DeleteTariffHandler(IServiceRepository serviceRepository, ITariffRepository tariffRepository,
            IRepository repository)
        {
            _serviceRepository = serviceRepository;
            _tariffRepository = tariffRepository;
            _repository = repository;
        }

        public async Task Handle(DeleteTariff command, CancellationToken cancellationToken)
        {
            await _tariffRepository.DeleteAsync(command.tariffId);
            await _repository.SaveChangesAsync();
        }
    }
}
