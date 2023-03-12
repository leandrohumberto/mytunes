using MediatR;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Commands.DeleteArtist
{
    public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, Unit>
    {
        private readonly IArtistRepository _artistRepository;

        public DeleteArtistCommandHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<Unit> Handle(DeleteArtistCommand request, CancellationToken cancellationToken = default)
        {
            if (await _artistRepository.ExistsAsync(request.Id, cancellationToken))
            {
                await _artistRepository.DeleteAsync(request.Id, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
