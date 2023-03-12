using MediatR;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.AlbumExists
{
    public class AlbumExistsQueryHandler : IRequestHandler<AlbumExistsQuery, bool>
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumExistsQueryHandler(IAlbumRepository albumRepository) => _albumRepository = albumRepository;

        public async Task<bool> Handle(AlbumExistsQuery request, CancellationToken cancellationToken = default)
            => await _albumRepository.ExistsAsync(request.Id, cancellationToken);
    }
}
