using AutoMapper;
using MediatR;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Commands
{
    public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, int>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public CreateAlbumCommandHandler(IAlbumRepository albumRepository, IArtistRepository artistRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAlbumCommand request, CancellationToken cancellationToken = default)
        {
            if (!await _artistRepository.ExistsAsync(request.IdArtist, cancellationToken))
            {
                throw new Exception($"No artists found for the given Id ({request.IdArtist}).");
            }

            //
            // Create tracklist
            var tracklist = request.Tracklist.Select(p => new Track(p.Number, p.Name, p.Length));

            //
            // Create Album object
            //var album = new Album(request.Name, request.IdArtist, request.Year, request.Genre, request.Format, tracklist);
            var album = _mapper.Map<Album>(request);

            //
            // Create on the repository
            return await _albumRepository.AddAsync(album, cancellationToken);
        }
    }
}
