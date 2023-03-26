using MyTunes.Core.Entities;

namespace MyTunes.Core.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(User user, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken = default);
        Task<User> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
