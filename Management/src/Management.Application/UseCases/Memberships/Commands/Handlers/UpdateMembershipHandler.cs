using FClub.Backend.Common.Exceptions;
using Management.Application.Services;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Entities;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.Memberships.Commands.Handlers
{
    public sealed class UpdateMembershipHandler : IRequestHandler<UpdateMembership, MembershipDto?>
    {
        private IHttpAccessControlClient _accessControlClient;
        private readonly IRepository _repository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IStatisticRepository _statisticRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly ITariffRepository _tariffRepository;

        public UpdateMembershipHandler(
            IRepository repository, IMembershipRepository membershipRepository,
            IStatisticRepository statisticRepository, IClientRepository clientRepository,
            IBranchRepository branchRepository, IHttpAccessControlClient accessControlClient,
            ITariffRepository tariffRepository)
        {
            _repository = repository;
            _membershipRepository = membershipRepository;
            _statisticRepository = statisticRepository;
            _clientRepository = clientRepository;
            _branchRepository = branchRepository;
            _accessControlClient = accessControlClient;
            _tariffRepository = tariffRepository;
        }

        public async Task<MembershipDto?> Handle(UpdateMembership command, CancellationToken cancellationToken)
        {
            var (membershipId, tariffId, monthQuantity, clientId, branchId) = command;

            var membership = await _membershipRepository.GetAsync(membershipId, MembershipIncludes.Tariff | MembershipIncludes.Client)
                ?? throw new NotFoundException($"Cannot find membership {membershipId}");

            Tariff? tariff = null;
            if (membership.Tariff != null)
            {
                tariff = await _tariffRepository.GetAsync(tariffId, TariffIncludes.Services)
                    ?? throw new NotFoundException($"Cannot find tariff {tariffId}");
            }

            var client = await _clientRepository.GetAsync(clientId, ClientIncludes.SocialGroup)
                ?? throw new NotFoundException($"Cannot find client {clientId}");

            var branch = await _branchRepository.GetAsync(branchId)
                ?? throw new NotFoundException($"Cannot find branch {branchId}");

            membership.UpdateDetails(tariffId, monthQuantity, clientId, branchId);
            membership.SetCost();

            await _statisticRepository.AddAsync(StatisticNote.Create(membership.BranchId, membership.TotalCost));

            await _accessControlClient.UpdateMembership(
                new(
                    membership.Id,
                    membership.TariffId,
                    membership.ExpiresDate,
                    membership.ClientId,
                    membership.BranchId)
            );

            await _repository.SaveChangesAsync();

            if (tariff != null)
            {
                var services = tariff.ServiceTariffs.Select(st => st.Service).ToList();
                if (services.Count != 0)
                    return membership.AsDto(services);
            }
            return membership.AsDto();
        }
    }
}
