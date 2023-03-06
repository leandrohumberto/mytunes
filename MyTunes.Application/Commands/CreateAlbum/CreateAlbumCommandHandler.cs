using MediatR;
using MyTunes.Core.Entities;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Commands
{
    public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, int>
    {
        private readonly MyTunesDbContext _dbContext;

        public CreateAlbumCommandHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
        {
            if (!_dbContext.Artists.Any(p => p.Id == request.IdArtist))
            {
                throw new Exception($"No artists found for the given Id ({request.IdArtist}).");
            }

            //
            // Create tracklist
            var tracklist = request.Tracklist.Select(p => new Track(p.Number, p.Name, p.Length));

            //
            // Create Album object
            var album = new Album(request.Name, request.Year, request.Genre, request.Format, tracklist);

            //
            // Add album to the artist collection
            var artist = _dbContext.Artists.Single(p => p.Id == request.IdArtist);
            artist.Albums.Add(album);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
            return album.Id;
        }
    }
}
