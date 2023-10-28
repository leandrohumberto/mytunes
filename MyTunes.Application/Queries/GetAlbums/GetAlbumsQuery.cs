using MediatR;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Core.Enums;

namespace MyTunes.Application.Queries.GetAlbums
{
    public class GetAlbumsQuery : IRequest<IEnumerable<AlbumViewModel>>
    {
        public string? Title { get; set; }

        public string? Artist { get; set; }

        public uint? Year { get; set; }

        public string? Genre { get; set; }

        public AlbumFormat? Format { get; set; }
    }
}
