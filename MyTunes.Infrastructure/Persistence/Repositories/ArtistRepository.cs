using Microsoft.EntityFrameworkCore;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.Infrastructure.Persistence.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly MyTunesDbContext _dbContext;

        public ArtistRepository(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(Artist artist, CancellationToken cancellationToken = default)
        {
            _dbContext.Artists.Add(artist);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
            return artist.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var artist = await _dbContext.Artists.SingleAsync(a => a.Id == id, cancellationToken);
            _dbContext.Artists.Remove(artist);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
            => await _dbContext.Artists.AnyAsync(a => a.Id == id, cancellationToken);

        public async Task<IEnumerable<Artist>> GetAllAsync(string? name = null, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Artists.Include(a => a.Albums).ThenInclude(a => a.Tracklist)
                .Where(a => string.IsNullOrWhiteSpace(name) || a.Name == name)
                .ToListAsync(cancellationToken);
        }

        public async Task<Artist> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _dbContext.Artists.Include(a => a.Albums).ThenInclude(a => a.Tracklist)
                .SingleAsync(a => a.Id == id, cancellationToken);

        public async Task SaveChangesAsync(Artist artist, CancellationToken cancellationToken = default)
        {
            _dbContext.Update(artist);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
