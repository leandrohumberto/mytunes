using MediatR;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Commands.UpdateArtist
{
    public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand, Unit>
    {
        private readonly IArtistRepository _artistRepository;

        public UpdateArtistCommandHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<Unit> Handle(UpdateArtistCommand request, CancellationToken cancellationToken = default)
        {
            if (await _artistRepository.ExistsAsync(request.Id, cancellationToken))
            {
                var artist = await _artistRepository.GetByIdAsync(request.Id, cancellationToken);
                artist.Update(request.Name, request.Biography);
                await _artistRepository.SaveChangesAsync(artist, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
