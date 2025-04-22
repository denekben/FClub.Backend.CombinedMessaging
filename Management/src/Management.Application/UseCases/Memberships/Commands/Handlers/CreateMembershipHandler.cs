using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands.Handlers
{
    public sealed class CreateMembershipHandler : IRequestHandler<CreateMembership, MembershipDto?>
    {
        private IHttpAccessControlClient _accessControlClient;
        private IRepository _repository;
        private IMembershipRepository _membershipRepository;
        private IStatisticRepository _statisticRepository;
        private IClientRepository _clientRepository;
        private ITariffRepository _tariffRepository;
        private IBranchRepository _branchRepository;

        public CreateMembershipHandler(
            IRepository repository, IMembershipRepository membershipRepository,
            IClientRepository clientRepository, ITariffRepository tariffRepository, IStatisticRepository statisticRepository,
            IBranchRepository branchRepository, IHttpAccessControlClient accessControlClient)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
            _clientRepository = clientRepository;
            _tariffRepository = tariffRepository;
            _statisticRepository = statisticRepository;
            _branchRepository = branchRepository;
            _accessControlClient = accessControlClient;
        }

        public async Task<MembershipDto?> Handle(CreateMembership command, CancellationToken cancellationToken)
        {
            var (tariffId, monthQuantity, clientId, branchId) = command;

            var client = await _clientRepository.GetAsync(clientId, ClientIncludes.SocialGroup | ClientIncludes.Membership)
                ?? throw new NotFoundException($"Cannot find client {clientId}");

            var branch = await _branchRepository.GetAsync(branchId)
                ?? throw new NotFoundException($"Cannot find branch {branchId}");

            if (client.Membership != null)
                throw new BadRequestException($"Client already have membership");

            var tariff = await _tariffRepository.GetAsync(tariffId, TariffIncludes.Services)
                ?? throw new NotFoundException($"Cannot find tariff {tariffId}");

            var membership = Membership.Create(tariffId, monthQuantity, clientId, branchId);

            membership.Tariff = tariff;
            membership.Client = client;
            membership.SetCost();

            await _statisticRepository.AddAsync(StatisticNote.Create(membership.BranchId, membership.TotalCost));

            await _membershipRepository.AddAsync(membership);

            await _accessControlClient.CreateMembership(
                new(
                    membership.Id,
                    membership.TariffId,
                    membership.ExpiresDate,
                    membership.ClientId,
                    membership.BranchId)
            );

            await _repository.SaveChangesAsync();

            var services = membership.Tariff.ServiceTariffs.Select(st => st.Service).ToList();
            if (services.Count != 0)
                return membership.AsDto(services);
            return membership.AsDto();
        }
    }
}
