using MediatR;
using Notifications.Domain.Entities;
using Notifications.Domain.Repositories;

namespace Notifications.Application.IntegrationUseCases.Users.Handlers
{
    public sealed class RegisterNewUserHandler : IRequestHandler<RegisterNewUser>
    {
        private readonly IRepository _repository;
        private readonly IUserRepository _userRepository;

        public RegisterNewUserHandler(IRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task Handle(RegisterNewUser command, CancellationToken cancellationToken)
        {
            await _userRepository.AddAsync(AppUser.Create(command.UserId, false));
            await _repository.SaveChangesAsync();
        }
    }
}
