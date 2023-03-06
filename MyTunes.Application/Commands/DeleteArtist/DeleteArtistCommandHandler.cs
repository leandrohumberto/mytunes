using MediatR;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Commands.DeleteArtist
{
    public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, Unit>
    {
        private readonly MyTunesDbContext _dbContext;

        public DeleteArtistCommandHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
        {
            if (_dbContext.Artists.Any(p => p.Id == request.Id))
            {
                var album = _dbContext.Artists.Single(p => p.Id == request.Id);
                _dbContext.Artists.Remove(album);
                _ = await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
