using MyTunes.Application.InputModels.Track;
using MyTunes.Core.Enums;

namespace MyTunes.Application.InputModels.Album
{
    public class UpdateAlbumInputModel
    {
        public UpdateAlbumInputModel(string title, uint year, string genre, AlbumFormat format, IEnumerable<TrackInputModel> tracklist)
        {
            Title = title;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = new List<TrackInputModel>(tracklist);
        }

        public string Title { get; private set; }

        public uint Year { get; private set; }

        public string Genre { get; private set; }

        public AlbumFormat Format { get; private set; }

        public IEnumerable<TrackInputModel> Tracklist { get; private set; }
    }
}
