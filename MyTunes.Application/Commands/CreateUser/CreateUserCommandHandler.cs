using MediatR;
using MyTunes.Core.Entities;
using MyTunes.Core.Exceptions;
using MyTunes.Core.Repositories;
using MyTunes.Core.Services;

namespace MyTunes.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            if (await _userRepository.ExistsAsync(request.Email, cancellationToken))
            {
                throw new InvalidUserEmailException(request.Email, "Email already exists.");
            }

            var computedPassword = _authService.ComputeSha256Hash(request.Password);
            var user = new User(request.Name, request.Email, computedPassword, request.Role);
            return await _userRepository.CreateAsync(user, cancellationToken);
        }
    }
}
