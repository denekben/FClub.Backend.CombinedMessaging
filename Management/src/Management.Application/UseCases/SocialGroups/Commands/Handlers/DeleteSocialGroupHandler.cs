using FClub.Backend.Common.Exceptions;
using Management.Domain.Repositories;
using MediatR;

namespace Management.Application.UseCases.SocialGroups.Commands.Handlers
{
    public sealed class DeleteSocialGroupHandler : IRequestHandler<DeleteSocialGroup>
    {
        private readonly ISocialGroupRepository _socialGroupRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IRepository _repository;

        public DeleteSocialGroupHandler(ISocialGroupRepository socialGroupRepository, IRepository repository,
            ITariffRepository tariffRepository)
        {
            _socialGroupRepository = socialGroupRepository;
            _repository = repository;
            _tariffRepository = tariffRepository;
        }

        public async Task Handle(DeleteSocialGroup command, CancellationToken cancellationToken)
        {
            var socialGroup = await _socialGroupRepository.GetAsync(command.socialGroupId)
                ?? throw new NotFoundException($"Cannot find social group {command.socialGroupId}");

            // this is shit but im too lazy to do it cool
            var tariffs = await _tariffRepository.GetAllAsync() ?? [];
            tariffs = tariffs.Where(t => t.DiscountForSocialGroup != null && t.DiscountForSocialGroup.ContainsKey(command.socialGroupId)).ToList();
            foreach (var tariff in tariffs)
            {
                tariff.DiscountForSocialGroup.Remove(command.socialGroupId);
                _tariffRepository.UpdateAsync(tariff);
            }
            await _socialGroupRepository.DeleteAsync(command.socialGroupId);
            await _repository.SaveChangesAsync();
        }
    }
}
