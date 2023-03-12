using Microsoft.EntityFrameworkCore;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyTunesDbContext _dbContext;

        public UserRepository(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(User user, CancellationToken cancellationToken = default)
        {
            _dbContext.Users.Add(user);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
            return user.Id;
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
            => await _dbContext.Users.AnyAsync(u => u.Id == id, cancellationToken);

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _dbContext.Users.SingleAsync(u => u.Id == id, cancellationToken: cancellationToken);

        public async Task<bool> LoginAsync(string email, string password, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Users.AnyAsync(p => p.Email == email && p.Password == password, cancellationToken);
        }
    }
}
