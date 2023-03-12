using MediatR;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Commands.DeleteAlbum
{
    public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand, Unit>
    {
        private readonly IAlbumRepository _albumRepository;

        public DeleteAlbumCommandHandler(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<Unit> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken = default)
        {
            if (await _albumRepository.ExistsAsync(request.Id, cancellationToken))
            {
                await _albumRepository.DeleteAsync(request.Id, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
