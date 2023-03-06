using MediatR;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Commands.DeleteAlbum
{
    public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand, Unit>
    {
        private readonly MyTunesDbContext _dbContext;

        public DeleteAlbumCommandHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
        {
            if (_dbContext.Albums.Any(p => p.Id == request.Id))
            {
                var album = _dbContext.Albums.Single(p => p.Id == request.Id);
                _dbContext.Albums.Remove(album);
                _ = await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
