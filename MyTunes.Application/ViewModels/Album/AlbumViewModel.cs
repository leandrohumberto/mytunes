using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Enums;

namespace MyTunes.Application.ViewModels.Album
{
    public class AlbumViewModel
    {
        public AlbumViewModel(int id, string name, uint year, string genre, AlbumFormat format, IEnumerable<TrackViewModel> tracklist)
        {
            Id = id;
            Name = name;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = new List<TrackViewModel>(tracklist);
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        public uint Year { get; private set; }

        public string Genre { get; private set; }

        public AlbumFormat Format { get; private set; }

        public IEnumerable<TrackViewModel> Tracklist { get; private set; }
    }
}