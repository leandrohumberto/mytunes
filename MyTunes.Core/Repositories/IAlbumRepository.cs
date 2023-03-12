using MyTunes.Core.Entities;
using MyTunes.Core.Enums;

namespace MyTunes.Core.Repositories
{
    public interface IAlbumRepository
    {
        Task<int> AddAsync(Album album, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Album>> GetAllAsync(string? name = default, string? artistName = default, uint? year = default, string? genre = default, AlbumFormat? format = default, CancellationToken cancellationToken = default);
        Task<Album> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(Album album, CancellationToken cancellationToken = default);
    }
}
