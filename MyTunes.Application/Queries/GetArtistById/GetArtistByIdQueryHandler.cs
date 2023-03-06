using MediatR;
using MyTunes.Application.ViewModels.Artist;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Queries.GetArtistById
{
    public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, ArtistViewModel>
    {
        private readonly MyTunesDbContext _dbContext;

        public GetArtistByIdQueryHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ArtistViewModel> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
        {
            if (_dbContext.Artists.Any(p => p.Id == request.Id))
            {
                var artist = _dbContext.Artists.Where(p => p.Id == request.Id).Single();
                return await Task.FromResult(new ArtistViewModel(artist.Id, artist.Name, artist.Biography));
            }

            throw new Exception($"No artist found for the given Id ({request.Id}).");
        }
    }
}
