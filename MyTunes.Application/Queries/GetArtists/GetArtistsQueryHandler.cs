using MediatR;
using MyTunes.Application.ViewModels.Artist;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Queries.GetArtists
{
    public class GetArtistsQueryHandler : IRequestHandler<GetArtistsQuery, IEnumerable<ArtistViewModel>>
    {
        private readonly MyTunesDbContext _dbContext;

        public GetArtistsQueryHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ArtistViewModel>> Handle(GetArtistsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<ArtistViewModel> result;

            if (request == null || string.IsNullOrWhiteSpace(request?.Name))
            {
                result = _dbContext.Artists
                    .Select(artist => new ArtistViewModel(artist.Id, artist.Name, artist.Biography));
            }
            else
            {
                result = _dbContext.Artists
                    .Where(artist => artist.Name == request.Name.Trim())
                    .Select(artist => new ArtistViewModel(artist.Id, artist.Name, artist.Biography));
            }

            return await Task.FromResult(result);
        }
    }
}
