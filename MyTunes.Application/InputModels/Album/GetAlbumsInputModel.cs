using MyTunes.Core.Enums;

namespace MyTunes.Application.InputModels.Album
{
    public class GetAlbumsInputModel
    {
        public string? Title { get; set; }

        public string? Artist { get; set; }

        public uint? Year { get; set; }

        public string? Genre { get; set; }

        public AlbumFormat? Format { get; set; }
    }
}
