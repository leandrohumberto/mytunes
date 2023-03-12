using MediatR;
using MyTunes.Application.ViewModels.Artist;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.GetArtists
{
    public class GetArtistsQueryHandler : IRequestHandler<GetArtistsQuery, IEnumerable<ArtistViewModel>>
    {
        private readonly IArtistRepository _artistRepository;

        public GetArtistsQueryHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<IEnumerable<ArtistViewModel>> Handle(GetArtistsQuery request, CancellationToken cancellationToken = default)
        {
            var artist = await _artistRepository.GetAllAsync(request.Name, cancellationToken);
            return artist.Select(artist => new ArtistViewModel(artist.Id, artist.Name, artist.Biography));
        }
    }
}
