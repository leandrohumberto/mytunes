using AutoMapper;
using MediatR;
using MyTunes.Application.ViewModels.Artist;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.GetArtists
{
    public class GetArtistsQueryHandler : IRequestHandler<GetArtistsQuery, IEnumerable<ArtistViewModel>>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public GetArtistsQueryHandler(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArtistViewModel>> Handle(GetArtistsQuery request, CancellationToken cancellationToken = default)
        {
            var artist = await _artistRepository.GetAllAsync(request.Name, cancellationToken);
            return artist.Select(artist => _mapper.Map<ArtistViewModel>(artist));
        }
    }
}
