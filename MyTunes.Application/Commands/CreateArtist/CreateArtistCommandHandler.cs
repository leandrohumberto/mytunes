using MediatR;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Commands.CreateArtist
{
    public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, int>
    {
        private readonly IArtistRepository _artistRepository;

        public CreateArtistCommandHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<int> Handle(CreateArtistCommand request, CancellationToken cancellationToken = default)
        {
            var artist = new Artist(request.Name, request.Biography);
            return await _artistRepository.AddAsync(artist, cancellationToken);
        }
    }
}
