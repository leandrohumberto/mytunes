using MyTunes.Core.Enums;

namespace MyTunes.Core.Entities
{
    public class Album : BaseEntity
    {
        public Album(string name, Artist artist, uint year, string genre, AlbumFormat format, IEnumerable<Track> tracklist)
        {
            Name = name;
            Artist = artist;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = new List<Track>(tracklist);
        }

        public string Name { get; private set; }

        public Artist Artist { get; private set; }

        public uint Year { get; private set; }

        public string Genre { get; private set; }

        public AlbumFormat Format { get; private set; }

        public IEnumerable<Track> Tracklist { get; private set; }

        public void Update(string name, Artist artist, uint year, string genre, AlbumFormat format, IEnumerable<Track> tracklist)
        {
            Name = name;
            Artist = artist;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = new List<Track>(tracklist);
        }
    }
}
