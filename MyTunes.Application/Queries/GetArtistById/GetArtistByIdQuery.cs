using MediatR;
using MyTunes.Application.ViewModels.Artist;

namespace MyTunes.Application.Queries.GetArtistById
{
    public class GetArtistByIdQuery : IRequest<ArtistViewModel>
    {
        public GetArtistByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
