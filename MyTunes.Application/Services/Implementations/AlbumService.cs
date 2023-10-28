using Microsoft.EntityFrameworkCore;
using MyTunes.Application.InputModels.Album;
using MyTunes.Application.Services.Interfaces;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Entities;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Services.Implementations
{
    public class AlbumService : IAlbumService
    {
        private readonly MyTunesDbContext _dbContext;

        public AlbumService(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Create(CreateAlbumInputModel inputModel, CancellationToken cancellationToken = default)
        {
            if (!_dbContext.Artists.Any(p => p.Id == inputModel.IdArtist))
            {
                throw new Exception($"No artists found for the given Id ({inputModel.IdArtist}).");
            }

            //
            // Create tracklist
            var tracklist = inputModel.Tracklist.Select(p => new Track(p.Number, p.Name, p.Length));

            //
            // Create Album object
            var album = new Album(inputModel.Name, inputModel.Year, inputModel.Genre,
                inputModel.Format, tracklist);

            //
            // Add album to the artist collection
            var artist = _dbContext.Artists.Single(p => p.Id == inputModel.IdArtist);
            artist.Albums.Add(album);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
            return album.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Albums.Any(p => p.Id == id))
            {
                var album = _dbContext.Albums.Single(p => p.Id == id);
                _dbContext.Albums.Remove(album);
                _ = await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<AlbumViewModel>> Get(GetAlbumsInputModel? inputModel)
        {
            if (inputModel == null)
            {
                return await Task.FromResult<IEnumerable<AlbumViewModel>>(
                    _dbContext.Albums
                        .Include(a => a.Artist)
                        .Include(a => a.Tracklist)
                        .Select(p => new AlbumViewModel(
                            p.Id,
                            p.Title,
                            p.Artist != null ? p.Artist.Name : string.Empty,
                            p.Year,
                            p.Genre,
                            p.Format,
                            p.Tracklist.Select(t => new TrackViewModel(t.Number, t.Title, t.Length))))
                        .ToList());
            }

            var albums = Enumerable.Empty<Album>();

            if (!string.IsNullOrWhiteSpace(inputModel.Name))
            {
                bool predicate(Album a) => a.Name == inputModel.Name;
                albums = albums.Any() ? albums.Where(predicate)
                    : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist).Where(predicate);
            }

            if (inputModel.Year.HasValue && inputModel.Year > 0)
            {
                bool predicate(Album a) => a.Year == inputModel.Year;
                albums = albums.Any() ? albums.Where(predicate)
                    : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist).Where(predicate);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Artist))
            {
                bool predicate(Album album) => _dbContext.Artists.Single(artist => artist.Id == album.IdArtist).Name == inputModel.Artist;
                albums = albums.Any() ? albums.Where(predicate)
                    : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist).Where(predicate);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Genre))
            {
                bool predicate(Album a) => a.Genre == inputModel.Genre;
                albums = albums.Any() ? albums.Where(predicate)
                    : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist).Where(predicate);
            }

            if (inputModel.Format.HasValue)
            {
                bool predicate(Album a) => a.Format == inputModel.Format;
                albums = albums.Any() ? albums.Where(predicate)
                    : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist).Where(predicate);
            }

            albums = albums.Any() ? albums : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist);

            return await Task.FromResult(
                albums.Select(p => new AlbumViewModel(
                    p.Id,
                    p.Title,
                    p.Artist != null ? p.Artist.Name : string.Empty,
                    p.Year,
                    p.Genre,
                    p.Format,
                    p.Tracklist.Select(t => new TrackViewModel(t.Number, t.Title, t.Length))))
                    .ToList());
        }

        public async Task<AlbumViewModel> GetById(int id)
        {
            if (_dbContext.Albums.Any(p => p.Id == id))
            {
                var album = _dbContext.Albums
                    .Include(a => a.Artist)
                    .Include(a => a.Tracklist)
                    .Single(a => a.Id == id);

                return await Task.FromResult(new AlbumViewModel(
                    album.Id,
                    album.Title,
                    album.Artist != null ? album.Artist.Name : string.Empty,
                    album.Year,
                    album.Genre,
                    album.Format,
                    album.Tracklist.Select(p => new TrackViewModel(p.Number, p.Title, p.Length))
                    .ToList()));
            }

            throw new Exception($"No album found for the given Id ({id}).");
        }

        public async Task Update(int id, UpdateAlbumInputModel inputModel, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Albums.Any(p => p.Id == id))
            {
                var album = _dbContext.Albums.Single(p => p.Id == id);
                _dbContext.Tracks.RemoveRange(album.Tracklist);
                album.Update(
                    inputModel.Title,
                    inputModel.Year,
                    inputModel.Genre,
                    inputModel.Format,
                    inputModel.Tracklist.Select(p => new Track(p.Number, p.Title, p.Length)));

                _ = await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
