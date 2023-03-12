using MediatR;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken = default)
        {
            return await _userRepository.LoginAsync(request.Email, request.Password, cancellationToken);
        }
    }
}
