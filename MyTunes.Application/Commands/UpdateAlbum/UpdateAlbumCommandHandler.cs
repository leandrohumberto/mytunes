using MediatR;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Commands.UpdateAlbum
{
    public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Unit>
    {
        private readonly IAlbumRepository _albumRepository;

        public UpdateAlbumCommandHandler(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<Unit> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken = default)
        {
            if (await _albumRepository.ExistsAsync(request.Id, cancellationToken))
            {
                var album = await _albumRepository.GetByIdAsync(request.Id, cancellationToken);

                album.Update(
                    request.Name,
                    request.Year,
                    request.Genre,
                    request.Format,
                    request.Tracklist.Select(p => new Track(p.Number, p.Name, p.Length)));

                await _albumRepository.SaveChangesAsync(album, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
