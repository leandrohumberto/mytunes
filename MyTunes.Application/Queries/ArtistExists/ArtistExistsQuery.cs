using MediatR;

namespace MyTunes.Application.Queries.ArtistExists
{
    public class ArtistExistsQuery : IRequest<bool>
    {
        public ArtistExistsQuery(int id) => Id = id;

        public int Id { get; private set; }
    }
}
