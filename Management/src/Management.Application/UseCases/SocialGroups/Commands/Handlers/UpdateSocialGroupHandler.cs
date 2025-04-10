using FClub.Backend.Common.Exceptions;
using Management.Domain.DTOs;
using Management.Domain.DTOs.Mappers;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.SocialGroups.Commands.Handlers
{
    public sealed class UpdateSocialGroupHandler : IRequestHandler<UpdateSocialGroup, SocialGroupDto?>
    {
        private readonly ISocialGroupRepository _socialGroupRepository;
        private readonly IRepository _repository;

        public UpdateSocialGroupHandler(ISocialGroupRepository socialGroupRepository, IRepository repository)
        {
            _socialGroupRepository = socialGroupRepository;
            _repository = repository;
        }

        public async Task<SocialGroupDto?> Handle(UpdateSocialGroup command, CancellationToken cancellationToken)
        {
            var socialGroup = await _socialGroupRepository.GetAsync(command.Id)
                ?? throw new NotFoundException($"Cannot find social group {command.Id}");

            socialGroup.UpdateDetails(command.Name);
            await _repository.SaveChangesAsync();

            return socialGroup.AsDto();
        }
    }
}
