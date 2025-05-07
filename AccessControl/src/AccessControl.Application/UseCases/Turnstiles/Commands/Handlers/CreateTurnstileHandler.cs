using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Exceptions;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Commands.Handlers
{
    public sealed class CreateTurnstileHandler : IRequestHandler<CreateTurnstile, TurnstileDto?>
    {
        private readonly ITurnstileRepository _turnstileRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public CreateTurnstileHandler(
            ITurnstileRepository turnstileRepository, IBranchRepository branchRepository, IRepository repository,
            IServiceRepository serviceRepository)
        {
            _turnstileRepository = turnstileRepository;
            _branchRepository = branchRepository;
            _repository = repository;
            _serviceRepository = serviceRepository;
        }

        public async Task<TurnstileDto?> Handle(CreateTurnstile command, CancellationToken cancellationToken)
        {
            var (name, isMain, branchId, serviceId) = command;

            var branch = await _branchRepository.GetAsync(branchId, BranchIncludes.ServicesBranches)
                ?? throw new NotFoundException($"Cannot find branch {branchId}");
            Service? service = null;
            if (serviceId != null)
            {
                service = await _serviceRepository.GetAsync((Guid)serviceId)
                    ?? throw new NotFoundException($"Cannot find service {serviceId}");
            }
            if (isMain && serviceId != null)
                throw new BadRequestException($"Main turnstile cannot have service");
            if (!isMain && !branch.ServiceBranches.Any(sb => sb.ServiceId == serviceId))
                throw new NotFoundException($"Cannot find service {serviceId}");
            if (!isMain && service == null)
                throw new BadRequestException($"Service cannot be null for not main turnstiles");

            var turnstile = Turnstile.Create(name, branchId, serviceId, isMain);
            await _turnstileRepository.AddAsync(turnstile);
            await _repository.SaveChangesAsync();

            turnstile.Service = service;
            return turnstile.AsDto();

        }
    }
}
