using MediatR;
using MyTunes.Core.Entities;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Commands.UpdateAlbum
{
    public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Unit>
    {
        private readonly MyTunesDbContext _dbContext;

        public UpdateAlbumCommandHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
        {
            if (_dbContext.Albums.Any(p => p.Id == request.Id))
            {
                var album = _dbContext.Albums.Single(p => p.Id == request.Id);
                _dbContext.Tracks.RemoveRange(album.Tracklist);
                album.Update(
                    request.Name,
                    request.Year,
                    request.Genre,
                    request.Format,
                    request.Tracklist.Select(p => new Track(p.Number, p.Name, p.Length)));

                _ = await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
