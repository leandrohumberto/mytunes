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

        public async Task<int> Create(CreateUserInputModel inputModel, CancellationToken cancellationToken = default)
        {
            var user = new User(inputModel.Name, inputModel.Email, inputModel.Password, inputModel.Role);
            _dbContext.Users.Add(user);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        public async Task<UserViewModel> GetById(int id)
            => await Task.FromResult(_dbContext.Users
                .Where(p => p.Id == id)
                .Select(p => new UserViewModel(p.Name, p.Email, p.Role)).SingleOrDefault())
            ?? throw new Exception("User not found");

        public async Task<bool> Login(LoginUserInputModel inputModel)
            => await Task.FromResult(_dbContext.Users.Any(p => p.Email == inputModel.Email && p.Password == inputModel.Password));
    }
}
