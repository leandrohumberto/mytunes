using AutoMapper;
using MediatR;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Commands.UpdateAlbum
{
    public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, Unit>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public UpdateAlbumCommandHandler(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
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
                    request.Tracklist.Select(trackInputModel => _mapper.Map<Track>(trackInputModel)));

                await _albumRepository.SaveChangesAsync(album, cancellationToken);
            }

            return Unit.Value;
        }
    }
}
