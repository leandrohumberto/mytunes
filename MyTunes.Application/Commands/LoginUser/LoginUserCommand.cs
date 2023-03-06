using MediatR;

namespace MyTunes.Application.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<bool>
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
