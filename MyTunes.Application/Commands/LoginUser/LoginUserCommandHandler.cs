using MediatR;
using MyTunes.Application.ViewModels.User;
using MyTunes.Core.Exceptions;
using MyTunes.Core.Repositories;
using MyTunes.Core.Services;

namespace MyTunes.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken = default)
        {
            var computedPassword = _authService.ComputeSha256Hash(request.Password);
            var user = await _userRepository.GetByEmailAndPasswordAsync(request.Email, computedPassword, cancellationToken);
            
            if  (user == null)
            {
                throw new LoginFailException(request.Email, request.Password,
                    $"Incorrect {nameof(request.Email)} or {nameof(request.Password)}");
            }

            var token = _authService.GenerateJwtToken(user.Email, user.Role);
            return new LoginUserViewModel(user.Email, token);
        }
    }
}
