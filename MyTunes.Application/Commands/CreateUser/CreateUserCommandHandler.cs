using MediatR;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            var user = new User(request.Name, request.Email, request.Password, request.Role);
            return await _userRepository.CreateAsync(user, cancellationToken);
        }
    }
}
