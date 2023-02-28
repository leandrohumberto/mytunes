using MyTunes.Core.Enums;

namespace MyTunes.Application.ViewModels.User
{
    public class UserViewModel
    {
        public UserViewModel(string name, string email, UserRole role)
        {
            Name = name;
            Email = email;
            Role = role;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public UserRole Role { get; private set; }
    }
}
