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

        public int Create(CreateAlbumInputModel inputModel)
        {
            if (!_dbContext.Artists.Keys.Any(p => p == inputModel.IdArtist))
            {
                throw new Exception($"No artists found for the given Id ({inputModel.IdArtist}).");
            }

            //
            // Create tracklist
            var tracklist = inputModel.Tracklist.Select(p => new Track(p.Number, p.Name, p.Length));

            //
            // Create Album object
            var album = new Album(inputModel.Name, inputModel.IdArtist, inputModel.Year, inputModel.Genre,
                inputModel.Format, tracklist);

            //
            // Add album to the artist collection
            var artist = _dbContext.Artists.Single(p => p.Key == inputModel.IdArtist).Value;
            artist.Albums.Add(album);

            //
            // Add album to the DbContext Album dictionary
            var idAlbum = _dbContext.Albums.Keys.Any() ? _dbContext.Albums.Keys.Max() + 1 : 1;
            _dbContext.Albums.Add(idAlbum, album);
            return idAlbum;
        }

        public void Delete(int id)
        {
            if (_dbContext.Albums.Any(p => p.Key == id))
            {
                var album = _dbContext.Albums.Single(p => p.Key == id).Value;
                _dbContext.Albums.Remove(id);

                if (_dbContext.Artists.Any(p => p.Key == album.IdArtist))
                {
                    var artist = _dbContext.Artists.Single(p => p.Key == album.IdArtist).Value;
                    artist.Albums.Remove(album);
                }
            }
        }

        public IEnumerable<AlbumViewModel> Get(GetAlbumsInputModel? inputModel)
        {
            if (inputModel == null)
            {
                return _dbContext.Albums.Select(p => new AlbumViewModel(
                    p.Key,
                    p.Value.Name,
                    p.Value.Year,
                    p.Value.Genre,
                    p.Value.Format,
                    p.Value.Tracklist.Select(t => new TrackViewModel(t.Number, t.Name, t.Length))));
            }

            var albums = Enumerable.Empty<KeyValuePair<int, Album>>();

            if (!string.IsNullOrWhiteSpace(inputModel.Name))
            {
                bool predicate(KeyValuePair<int, Album> p) => p.Value.Name == inputModel.Name;
                albums = albums.Any() ? albums.Where(predicate) : _dbContext.Albums.Where(predicate);
            }

            if (inputModel.Year.HasValue && inputModel.Year > 0)
            {
                bool predicate(KeyValuePair<int, Album> p) => p.Value.Year == inputModel.Year;
                albums = albums.Any() ? albums.Where(predicate) : _dbContext.Albums.Where(predicate);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Artist))
            {
                bool predicate(KeyValuePair<int, Album> p) => _dbContext.Artists.Single(a => a.Key == p.Value.IdArtist).Value.Name == inputModel.Artist;
                albums = albums.Any() ? albums.Where(predicate) : _dbContext.Albums.Where(predicate);
            }

            if (!string.IsNullOrWhiteSpace(inputModel.Genre))
            {
                bool predicate(KeyValuePair<int, Album> p) => p.Value.Genre == inputModel.Genre;
                albums = albums.Any() ? albums.Where(predicate) : _dbContext.Albums.Where(predicate);
            }

            if (inputModel.Format.HasValue)
            {
                bool predicate(KeyValuePair<int, Album> p) => p.Value.Format == inputModel.Format;
                albums = albums.Any() ? albums.Where(predicate) : _dbContext.Albums.Where(predicate);
            }

            albums = albums.Any() ? albums : _dbContext.Albums;

            return albums.Select(p => new AlbumViewModel(
                p.Key,
                p.Value.Name,
                p.Value.Year,
                p.Value.Genre,
                p.Value.Format,
                p.Value.Tracklist.Select(t => new TrackViewModel(t.Number, t.Name, t.Length))));
        }

        public AlbumViewModel GetById(int id)
        {
            if (_dbContext.Albums.Any(p => p.Key == id))
            {
                var album = _dbContext.Albums.Single(p => p.Key == id).Value;
                return new AlbumViewModel(id, album.Name, album.Year, album.Genre, album.Format,
                    album.Tracklist.Select(p => new TrackViewModel(p.Number, p.Name, p.Length)));
            }

            throw new Exception($"No album found for the given Id ({id}).");
        }

        public void Update(int id, UpdateAlbumInputModel inputModel)
        {
            if (_dbContext.Albums.Any(p => p.Key == id))
            {
                var album = _dbContext.Albums.Single(p => p.Key == id).Value;
                album.Update(inputModel.Name, inputModel.Year, inputModel.Genre, inputModel.Format,
                    inputModel.Tracklist.Select(p => new Track(p.Number, p.Name, p.Length)));
            }
        }
    }
}
