using MediatR;
using MyTunes.Core.Entities;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Commands.CreateArtist
{
    public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, int>
    {
        private readonly MyTunesDbContext _dbContext;

        public CreateArtistCommandHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
        {
            var artist = new Artist(request.Name, request.Biography);
            _dbContext.Artists.Add(artist);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
            return artist.Id;
        }
    }
}
