using MediatR;
using MyTunes.Application.ViewModels.Artist;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.GetArtistById
{
    public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, ArtistViewModel>
    {
        private readonly IArtistRepository _repository;

        public GetArtistByIdQueryHandler(IArtistRepository repository)
        {
            _repository = repository;
        }

        public async Task<ArtistViewModel> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken = default)
        {
            var artist = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return new ArtistViewModel(artist.Id, artist.Name, artist.Biography);
        }
    }
}
