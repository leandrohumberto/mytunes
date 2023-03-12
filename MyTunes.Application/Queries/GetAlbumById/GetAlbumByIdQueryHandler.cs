using MediatR;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.GetAlbumById
{
    public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, AlbumViewModel>
    {
        private readonly IAlbumRepository _albumRepository;

        public GetAlbumByIdQueryHandler(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<AlbumViewModel> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken = default)
        {
            var album = await _albumRepository.GetByIdAsync(request.Id, cancellationToken);
            return new AlbumViewModel(
                album.Id,
                album.Name,
                album.Artist != null ? album.Artist.Name : string.Empty,
                album.Year,
                album.Genre,
                album.Format,
                album.Tracklist.Select(t => new TrackViewModel(t.Number, t.Name, t.Length)).ToList());
        }
    }
}
