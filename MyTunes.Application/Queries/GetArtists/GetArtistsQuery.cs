using MediatR;
using MyTunes.Application.ViewModels.Artist;

namespace MyTunes.Application.Queries.GetArtists
{
    public class GetArtistsQuery : IRequest<IEnumerable<ArtistViewModel>>
    {
        public string? Name { get; set; }
    }
}
