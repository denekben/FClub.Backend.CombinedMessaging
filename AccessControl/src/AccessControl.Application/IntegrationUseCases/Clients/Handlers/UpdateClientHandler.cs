using AccessControl.Application.IntegrationUseCases.Clients;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Exceptions;
using FClub.Backend.Common.Logging;
using MediatR;

namespace AccessControl.Application.UseCases.Clients.Commands.Handlers
{
    [SkipLogging]
    public sealed class UpdateClientHandler : IRequestHandler<UpdateClient>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRepository _repository;
        private readonly IMembershipRepository _membershipRepository;

        public UpdateClientHandler(IClientRepository clientRepository, IRepository repository,
            IMembershipRepository membershipRepository)
        {
            _clientRepository = clientRepository;
            _repository = repository;
            _membershipRepository = membershipRepository;
        }

        public async Task Handle(UpdateClient command, CancellationToken cancellationToken)
        {
            var (id, firstName, secondName, patronymic, phone, email, allowEntry, membership) = command;
            var updatingClient = await _clientRepository.GetAsync(id, ClientIncludes.Membership)
                ?? throw new NotFoundException($"Cannot find client {id}");

            var isStaff = updatingClient.IsStaff;
            updatingClient.UpdateDetails(firstName, secondName, patronymic, phone, email, allowEntry, isStaff, membership?.Id);

            if (membership != null)
            {
                var existingMembership = await _membershipRepository.GetAsync(membership.Id);
                if (existingMembership == null)
                {
                    await _membershipRepository.AddAsync(
                        Membership.Create(
                            membership.Id,
                            membership.TariffId,
                            membership.ExpiresDate,
                            membership.ClientId,
                            membership.BranchId));
                }
                else
                {
                    existingMembership.UpdateDetails(membership.TariffId, membership.ExpiresDate, membership.ClientId, membership.BranchId);
                }
            }
            else
            {
                if (updatingClient.Membership != null)
                {
                    await _membershipRepository.DeleteAsync(updatingClient.Membership.Id);
                }
            }

            await _repository.SaveChangesAsync();
        }
    }
}