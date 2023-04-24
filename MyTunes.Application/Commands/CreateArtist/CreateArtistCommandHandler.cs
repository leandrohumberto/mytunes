using AutoMapper;
using MediatR;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Commands.CreateArtist
{
    public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, int>
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IMapper _mapper;

        public CreateArtistCommandHandler(IArtistRepository artistRepository, IMapper mapper)
        {
            _artistRepository = artistRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateArtistCommand request, CancellationToken cancellationToken = default)
        {
            var artist = _mapper.Map<Artist>(request);
            return await _artistRepository.AddAsync(artist, cancellationToken);
        }
    }
}
