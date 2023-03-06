using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Queries.GetAlbumById
{
    public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, AlbumViewModel>
    {
        private readonly MyTunesDbContext _dbContext;

        public GetAlbumByIdQueryHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AlbumViewModel> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
        {
            if (_dbContext.Albums.Any(p => p.Id == request.Id))
            {
                var album = _dbContext.Albums
                    .Include(a => a.Artist)
                    .Include(a => a.Tracklist)
                    .Single(a => a.Id == request.Id);

                return await Task.FromResult(new AlbumViewModel(
                    album.Id,
                    album.Name,
                    album.Artist != null ? album.Artist.Name : string.Empty,
                    album.Year,
                    album.Genre,
                    album.Format,
                    album.Tracklist.Select(p => new TrackViewModel(p.Number, p.Name, p.Length))
                    .ToList()));
            }

            throw new Exception($"No album found for the given Id ({request.Id}).");
        }
    }
}
