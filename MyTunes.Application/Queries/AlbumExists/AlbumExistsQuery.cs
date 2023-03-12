using MediatR;

namespace MyTunes.Application.Queries.AlbumExists
{
    public class AlbumExistsQuery : IRequest<bool>
    {
        public AlbumExistsQuery(int id) => Id = id;

        public int Id { get; private set; }
    }
}
