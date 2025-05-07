using AccessControl.Domain.DTOs;
using AccessControl.Domain.DTOs.Mappers;
using AccessControl.Domain.Entities;
using AccessControl.Domain.Repositories;
using FClub.Backend.Common.Exceptions;
using MediatR;

namespace AccessControl.Application.UseCases.Turnstiles.Commands.Handlers
{
    public sealed class UpdateTurnstileHandler : IRequestHandler<UpdateTurnstile, TurnstileDto?>
    {
        private readonly ITurnstileRepository _turnstileRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IRepository _repository;

        public UpdateTurnstileHandler(ITurnstileRepository turnstileRepository,
            IRepository repository, IBranchRepository branchRepository, IServiceRepository serviceRepository)
        {
            _turnstileRepository = turnstileRepository;
            _repository = repository;
            _branchRepository = branchRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<TurnstileDto?> Handle(UpdateTurnstile command, CancellationToken cancellationToken)
        {
            var (id, name, isMain, branchId, serviceId) = command;

            var turnstile = await _turnstileRepository.GetAsync(id)
                ?? throw new NotFoundException($"Cannot find turnstile {id}");

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

            turnstile.UpdateDetails(name, branchId, serviceId, isMain);
            await _repository.SaveChangesAsync();

            turnstile.Service = service;
            return turnstile.AsDto();
        }
    }
}
