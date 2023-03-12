using MediatR;
using MyTunes.Application.Queries.AlbumExists;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.ArtistExists
{
    public class ArtistExistsQueryHandler : IRequestHandler<ArtistExistsQuery, bool>
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistExistsQueryHandler(IArtistRepository artistRepository) => _artistRepository = artistRepository;

        public async Task<bool> Handle(ArtistExistsQuery request, CancellationToken cancellationToken = default)
            => await _artistRepository.ExistsAsync(request.Id, cancellationToken);
    }
}
