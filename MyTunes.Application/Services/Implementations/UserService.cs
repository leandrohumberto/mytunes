using MyTunes.Application.InputModels.User;
using MyTunes.Application.Services.Interfaces;
using MyTunes.Application.ViewModels.User;
using MyTunes.Core.Entities;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly MyTunesDbContext _dbContext;

        public UserService(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(CreateUserInputModel inputModel)
        {
            var id = _dbContext.Users.Keys.Any() ? _dbContext.Users.Keys.Max() + 1 : 1;
            _dbContext.Users.Add(id, new User(inputModel.Name, inputModel.Email,
                inputModel.Password, inputModel.Role));

            return id;
        }

        public UserViewModel GetById(int id)
            => _dbContext.Users
                .Where(p => p.Key == id)
                .Select(p => new UserViewModel(p.Value.Name, p.Value.Email, p.Value.Role)).SingleOrDefault()
            ?? throw new Exception("User not found");

        public bool Login(LoginUserInputModel inputModel) =>
            _dbContext.Users.Values
                .Any(p => p.Email == inputModel.Email && p.Password == inputModel.Password);
    }
}
