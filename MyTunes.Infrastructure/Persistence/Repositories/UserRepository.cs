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

        public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default)
            => await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);

        public async Task<User?> GetByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken = default)
            => await _dbContext.Users.SingleOrDefaultAsync(p => p.Email == email && p.Password == password, cancellationToken);

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _dbContext.Users.SingleAsync(u => u.Id == id, cancellationToken: cancellationToken);
    }
}
