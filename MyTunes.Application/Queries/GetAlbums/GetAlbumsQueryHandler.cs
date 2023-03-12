using MediatR;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.GetAlbums
{
    public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, IEnumerable<AlbumViewModel>>
    {
        private readonly IAlbumRepository _albumRepository;

        public GetAlbumsQueryHandler(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<IEnumerable<AlbumViewModel>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken = default)
        {

            var albums = await _albumRepository.GetAllAsync(
                name: request?.Name,
                artistName: request?.Artist,
                year: request?.Year,
                genre: request?.Genre,
                format: request?.Format,
                cancellationToken: cancellationToken);

            return albums.Select(a => new AlbumViewModel(
                a.Id,
                a.Name,
                a.Artist != null ? a.Artist.Name : string.Empty,
                a.Year,
                a.Genre,
                a.Format,
                a.Tracklist.Select(t => new TrackViewModel(t.Number, t.Name, t.Length)).ToList()))
                .ToList();
        }
    }
}
