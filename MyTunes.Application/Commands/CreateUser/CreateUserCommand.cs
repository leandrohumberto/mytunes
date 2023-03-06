using MediatR;
using MyTunes.Core.Enums;

namespace MyTunes.Application.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public CreateUserCommand(string name, string email, string password, UserRole role)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            Active = true;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public UserRole Role { get; private set; }

        public bool Active { get; private set; }
    }
}
