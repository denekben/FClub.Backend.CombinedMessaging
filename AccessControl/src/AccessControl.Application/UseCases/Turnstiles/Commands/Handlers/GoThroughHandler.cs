using AccessControl.Application.Services;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Enums;
using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Exceptions;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Commands.Handlers
{
    public sealed class GoThroughHandler : IRequestHandler<GoThrough>
    {
        private readonly IHttpNotificationsClient _notificationsClient;
        private readonly IWSNotificationService _notifications;
        private readonly ITurnstileRepository _turnstileRepository;
        private readonly IEntryLogRepository _entryLogRepository;
        private readonly IStatisticNoteRepository _statisticNoteRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IRepository _repository;

        public GoThroughHandler(ITurnstileRepository turnstileRepository,
            IEntryLogRepository entryLogRepository, IStatisticNoteRepository statisticNoteRepository,
            IClientRepository clientRepository, IRepository repository, IWSNotificationService notifications,
            IHttpNotificationsClient notificationClient)
        {
            _turnstileRepository = turnstileRepository;
            _entryLogRepository = entryLogRepository;
            _statisticNoteRepository = statisticNoteRepository;
            _clientRepository = clientRepository;
            _repository = repository;
            _notifications = notifications;
            _notificationsClient = notificationClient;
        }

        public async Task Handle(GoThrough command, CancellationToken cancellationToken)
        {
            var (clientId, turnstileId, entryType) = command;
            var turnstile = await _turnstileRepository.GetAsync(turnstileId, TurnistileIncludes.Services)
                ?? throw new NotFoundException($"Cannot find turnstile {turnstileId}");

            var client = await _clientRepository.GetAsync(clientId, ClientIncludes.ServiceTariff)
                ?? throw new NotFoundException($"Cannot find client {clientId}");

            if (entryType == EntryType.Enter)
            {
                if (!client.AllowEntry)
                    throw new BadRequestException($"Client {client.Id} is not allowed to entry");

                if (!client.IsStaff)
                {
                    if (client.Membership == null)
                        throw new BadRequestException($"Client {client.Id} does not have a membership");
                    if (client.Membership.Tariff == null)
                        throw new BadRequestException($"Membership {client.Membership.Id} does not have a tariff");
                    if (!client.Membership.Tariff.AllowMultiBranches && client.Membership.BranchId != turnstile.BranchId)
                        throw new BadRequestException($"Client {client.Id} is not allowed to entry to branch {turnstile.BranchId}");
                    if (turnstile.ServiceId != null && !client.Membership.Tariff.ServiceTariffs.Select(st => st.ServiceId).ToList().Contains((Guid)turnstile.ServiceId))
                        throw new BadRequestException($"Client {client.Id} is not allowed to entry service {turnstile.ServiceId}");
                    if (client.Membership.ExpiresDate < DateTime.UtcNow)
                        throw new BadRequestException($"Client's {client.Id} membership expired");
                    if (turnstile.Branch.CurrentClientQuantity >= turnstile.Branch.MaxOccupancy)
                        throw new BadRequestException($"Branch {turnstile.BranchId} has reached occupancy limit");

                    await _statisticNoteRepository.AddAsync(StatisticNote.Create(turnstile.BranchId));
                    await _notifications.ClientEntered(turnstile.BranchId);

                    turnstile.Branch.Enter();
                }
            }
            else
            {
                if (!client.IsStaff)
                {
                    await _notifications.ClientExited(turnstile.BranchId);
                    turnstile.Branch.Exit();
                }
            }

            await _entryLogRepository.AddAsync(
                EntryLog.Create(
                    clientId,
                    turnstileId,
                    client.FullName.ToString(),
                    turnstile.Branch.Name,
                    turnstile.Branch.ServiceBranches.Select(st => st.Service).FirstOrDefault(s => s.Id == turnstile.ServiceId)?.Name,
                    entryType
            ));

            if (!client.IsStaff)
            {
                if (entryType == EntryType.Enter)
                    await _notificationsClient.GoThrough(
                        new(clientId)
                    );
            }

            await _repository.SaveChangesAsync();
        }
    }
}
