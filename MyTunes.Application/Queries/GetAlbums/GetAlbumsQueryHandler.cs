using AutoMapper;
using MediatR;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.GetAlbums
{
    public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, IEnumerable<AlbumViewModel>>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IMapper _mapper;

        public GetAlbumsQueryHandler(IAlbumRepository albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlbumViewModel>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken = default)
        {

            var albums = await _albumRepository.GetAllAsync(
                title: request?.Title,
                artistName: request?.Artist,
                year: request?.Year,
                genre: request?.Genre,
                format: request?.Format,
                cancellationToken: cancellationToken);

            return albums.Select(a => _mapper.Map<AlbumViewModel>(a)).ToList();
        }
    }
}
