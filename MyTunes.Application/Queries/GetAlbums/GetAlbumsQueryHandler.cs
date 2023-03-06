using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Entities;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Queries.GetAlbums
{
    public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, IEnumerable<AlbumViewModel>>
    {
        private readonly MyTunesDbContext _dbContext;

        public GetAlbumsQueryHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AlbumViewModel>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return await Task.FromResult<IEnumerable<AlbumViewModel>>(
                    _dbContext.Albums
                        .Include(a => a.Artist)
                        .Include(a => a.Tracklist)
                        .Select(p => new AlbumViewModel(
                            p.Id,
                            p.Name,
                            p.Artist != null ? p.Artist.Name : string.Empty,
                            p.Year,
                            p.Genre,
                            p.Format,
                            p.Tracklist.Select(t => new TrackViewModel(t.Number, t.Name, t.Length))))
                        .ToList());
            }

            var albums = Enumerable.Empty<Album>();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                bool predicate(Album a) => a.Name == request.Name;
                albums = albums.Any() ? albums.Where(predicate)
                    : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist).Where(predicate);
            }

            if (request.Year.HasValue && request.Year > 0)
            {
                bool predicate(Album a) => a.Year == request.Year;
                albums = albums.Any() ? albums.Where(predicate)
                    : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist).Where(predicate);
            }

            if (!string.IsNullOrWhiteSpace(request.Artist))
            {
                bool predicate(Album album) => album.Artist != null && album.Artist.Name == request.Artist;
                albums = albums.Any() ? albums.Where(predicate)
                    : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist).Where(predicate);
            }

            if (!string.IsNullOrWhiteSpace(request.Genre))
            {
                bool predicate(Album a) => a.Genre == request.Genre;
                albums = albums.Any() ? albums.Where(predicate)
                    : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist).Where(predicate);
            }

            if (request.Format.HasValue)
            {
                bool predicate(Album a) => a.Format == request.Format;
                albums = albums.Any() ? albums.Where(predicate)
                    : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist).Where(predicate);
            }

            albums = albums.Any() ? albums : _dbContext.Albums.Include(a => a.Artist).Include(a => a.Tracklist);

            return await Task.FromResult(
                albums.Select(p => new AlbumViewModel(
                    p.Id,
                    p.Name,
                    p.Artist != null ? p.Artist.Name : string.Empty,
                    p.Year,
                    p.Genre,
                    p.Format,
                    p.Tracklist.Select(t => new TrackViewModel(t.Number, t.Name, t.Length))))
                    .ToList());
        }
    }
}
