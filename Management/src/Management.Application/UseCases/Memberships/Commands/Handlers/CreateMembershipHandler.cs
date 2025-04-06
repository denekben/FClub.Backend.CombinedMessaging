using FClub.Backend.Common.Exceptions;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using Management.Shared.DTOs;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands.Handlers
{
    public sealed class CreateMembershipHandler : IRequestHandler<CreateMembership, MembershipDto?>
    {
        private IRepository _repository;
        private IMembershipRepository _membershipRepository;
        private IStatisticRepository _statisticRepository;
        private IClientRepository _clientRepository;
        private ITariffRepository _tariffRepository;
        private IBranchRepository _branchRepository;

        public CreateMembershipHandler(
            IRepository repository, IMembershipRepository membershipRepository,
            IClientRepository clientRepository, ITariffRepository tariffRepository, IStatisticRepository statisticRepository,
            IBranchRepository branchRepository)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
            _clientRepository = clientRepository;
            _tariffRepository = tariffRepository;
            _statisticRepository = statisticRepository;
            _branchRepository = branchRepository;
        }

        public async Task<MembershipDto?> Handle(CreateMembership command, CancellationToken cancellationToken)
        {
            var (tariffId, monthQuantity, expiresDate, clientId, branchId) = command;

            var client = await _clientRepository.GetAsync(clientId, ClientIncludes.SocialGroup)
                ?? throw new NotFoundException($"Cannot find client {clientId}");

            var branch = await _branchRepository.GetAsync(branchId)
                ?? throw new NotFoundException($"Cannot find branch {branchId}");

            if (client.MembershipId != null)
                throw new BadRequestException($"Client already have membership");

            var tariff = await _tariffRepository.GetAsync(tariffId)
                ?? throw new NotFoundException($"Cannot find tariff {tariffId}");

            var membership = Membership.Create(tariffId, expiresDate, clientId, branchId);

            membership.Tariff = tariff;
            membership.Client = client;
            membership.SetCost();

            await _statisticRepository.AddAsync(StatisticNote.Create(membership.TotalCost));

            await _membershipRepository.AddAsync(membership);
            await _repository.SaveChangesAsync();

            return membership.AsDto();
        }
    }
}
