using MediatR;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Commands.UpdateArtist
{
    public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand, Unit>
    {
        private readonly MyTunesDbContext _dbContext;

        public UpdateArtistCommandHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
        {
            if (_dbContext.Artists.Any(p => p.Id == request.Id))
            {
                var artist = _dbContext.Artists.Where(p => p.Id == request.Id).Single();
                artist.Update(request.Name, request.Biography);
                _ = await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
