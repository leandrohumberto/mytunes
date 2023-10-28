using MyTunes.Core.Enums;

namespace MyTunes.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string email, string password, UserRole role)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException($"'{nameof(email)}' cannot be null or empty.", nameof(email));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));
            }

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

        public void ChangePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));
            }

            Password = password;
        }
    }
}
