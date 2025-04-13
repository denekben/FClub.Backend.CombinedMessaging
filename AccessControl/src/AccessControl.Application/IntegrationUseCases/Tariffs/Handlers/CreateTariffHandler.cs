using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.IntegrationUseCases.Tariffs.Handler
{
    [SkipLogging]
    public sealed class CreateTariffHandler : IRequestHandler<CreateTariff>
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IRepository _repository;

        public CreateTariffHandler(ITariffRepository tariffRepository, IRepository repository)
        {
            _tariffRepository = tariffRepository;
            _repository = repository;
        }

        public async Task Handle(CreateTariff command, CancellationToken cancellationToken)
        {
            var (id, name, allowMultiBranches, serviceTariffs, services) = command;
            var tariff = Tariff.Create(id, name, allowMultiBranches);

            foreach (var serviceTariff in serviceTariffs)
            {
                var service = services.First(s => s.Id == serviceTariff.ServiceId);
                serviceTariff.Service = service;
            }
            tariff.ServiceTariffs = serviceTariffs;

            await _tariffRepository.AddAsync(tariff);

            await _repository.SaveChangesAsync();
        }
    }
}