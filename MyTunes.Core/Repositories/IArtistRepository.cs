using MyTunes.Core.Entities;

namespace MyTunes.Core.Repositories
{
    public interface IArtistRepository
    {
        Task<int> AddAsync(Artist artist, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Artist>> GetAllAsync(string? name = default, CancellationToken cancellationToken = default);
        Task<Artist> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(Artist artist, CancellationToken cancellationToken = default);
    }
}
