using AutoMapper;
using MediatR;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.GetAlbumById
{
    public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, AlbumViewModel>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public GetAlbumByIdQueryHandler(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        public async Task<AlbumViewModel> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken = default)
        {
            var album = await _albumRepository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<AlbumViewModel>(album);
        }
    }
}
