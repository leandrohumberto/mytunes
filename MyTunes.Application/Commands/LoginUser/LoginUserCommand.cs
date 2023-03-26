using MediatR;
using MyTunes.Application.ViewModels.User;

namespace MyTunes.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserViewModel>
    {
        public LoginUserCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }
    }
}
