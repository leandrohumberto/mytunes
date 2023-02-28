namespace MyTunes.Application.InputModels.User
{
    public class LoginUserInputModel
    {
        public LoginUserInputModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }
    }
}
