using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Enums;

namespace MyTunes.Application.ViewModels.Album
{
    public class AlbumViewModel
    {
        public AlbumViewModel(int id, string name, string artist, uint year, string genre, AlbumFormat format, IEnumerable<TrackViewModel> tracklist)
        {
            Id = id;
            Name = name;
            ArtistName = artist;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = tracklist != null ? new List<TrackViewModel>(tracklist) : new List<TrackViewModel>();
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string ArtistName { get; private set; }

        public uint Year { get; private set; }

        public string Genre { get; private set; }

        public AlbumFormat Format { get; private set; }

        public IEnumerable<TrackViewModel> Tracklist { get; private set; }
    }
}