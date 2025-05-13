using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using MediatR;
using Notifications.Application.IntegrationUseCases.AppUsers;

namespace Logging.Application.IntegrationUseCases.AppUsers.Handlers
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
