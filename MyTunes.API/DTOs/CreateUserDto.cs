namespace MyTunes.API.DTOs
{
    public class CreateUserDto
    {
        public CreateUserDto(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; private set; }

        public string Password { get; private set; }
    }
}
