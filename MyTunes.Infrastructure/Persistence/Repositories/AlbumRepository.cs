using Microsoft.EntityFrameworkCore;
using MyTunes.Core.Entities;
using MyTunes.Core.Enums;
using MyTunes.Core.Repositories;

namespace MyTunes.Infrastructure.Persistence.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly MyTunesDbContext _dbContext;

        public AlbumRepository(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(Album album, CancellationToken cancellationToken = default)
        {
            _dbContext.Albums.Add(album);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
            return album.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var album = await _dbContext.Albums.SingleAsync(a => a.Id == id, cancellationToken);
            _dbContext.Albums.Remove(album);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
            => await _dbContext.Albums.AnyAsync(a => a.Id == id, cancellationToken);

        public async Task<IEnumerable<Album>> GetAllAsync(string? title = default, string? artistName = default, uint? year = default, string? genre = default, AlbumFormat? format = default, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist)
                .Where(a => string.IsNullOrWhiteSpace(title) || title == a.Title)
                .Where(a => string.IsNullOrWhiteSpace(artistName) || a.Artist != null && a.Artist.Name == artistName)
                .Where(a => !year.HasValue || a.Year == year.Value)
                .Where(a => string.IsNullOrWhiteSpace(genre) || a.Genre == genre)
                .Where(a => !format.HasValue || a.Format == format.Value)
                .ToListAsync(cancellationToken);
        }

        public async Task<Album> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist)
                .SingleAsync(a => a.Id == id, cancellationToken);

        public async Task SaveChangesAsync(Album album, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(album);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
