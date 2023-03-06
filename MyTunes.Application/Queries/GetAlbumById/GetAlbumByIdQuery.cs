using MediatR;
using MyTunes.Application.ViewModels.Album;

namespace MyTunes.Application.Queries.GetAlbumById
{
    public class GetAlbumByIdQuery : IRequest<AlbumViewModel>
    {
        public GetAlbumByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
