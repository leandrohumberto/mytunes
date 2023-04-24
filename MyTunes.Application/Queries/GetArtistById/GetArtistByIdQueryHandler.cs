using AutoMapper;
using MediatR;
using MyTunes.Application.ViewModels.Artist;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.GetArtistById
{
    public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, ArtistViewModel>
    {
        private readonly IArtistRepository _repository;
        private readonly IMapper _mapper;

        public GetArtistByIdQueryHandler(IArtistRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ArtistViewModel> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken = default)
        {
            var artist = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<ArtistViewModel>(artist);
        }
    }
}
