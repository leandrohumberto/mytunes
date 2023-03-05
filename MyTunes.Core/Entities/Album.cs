using MyTunes.Core.Enums;

namespace MyTunes.Core.Entities
{
    public class Album : BaseEntity
    {
        public Album(string name, uint year, string genre, AlbumFormat format)
        {
            Validate(name, year, genre);
            Name = name;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = new List<Track>();

        }

        public Album(string name, uint year, string genre, AlbumFormat format, IEnumerable<Track> tracklist)
            : this(name, year, genre, format)
        {
            Validate(tracklist);
            Tracklist = new List<Track>(tracklist);
        }

        public string Name { get; private set; }

        public int IdArtist { get; private set; }

        public Artist? Artist { get; private set; }

        public uint Year { get; private set; }

        public string Genre { get; private set; }

        public AlbumFormat Format { get; private set; }

        public List<Track> Tracklist { get; private set; }

        public void Update(string name, uint year, string genre, AlbumFormat format, IEnumerable<Track> tracklist)
        {
            Validate(name, year, genre);
            Validate(tracklist);

            Name = name;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = new List<Track>(tracklist);
        }

        private static void Validate(string name, uint year, string genre)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (year == 0)
            {
                throw new ArgumentException($"'{nameof(year)}' cannot be zero.", nameof(name));
            }

            if (string.IsNullOrEmpty(genre))
            {
                throw new ArgumentException($"'{nameof(genre)}' cannot be null or empty.", nameof(genre));
            }
        }

        private static void Validate(IEnumerable<Track> tracklist)
        {
            tracklist = tracklist ?? throw new ArgumentNullException(nameof(tracklist));

            if (!tracklist.Any())
            {
                throw new ArgumentException($"'{nameof(tracklist)}' cannot be empty.", nameof(tracklist));
            }

            var number = 1;

            foreach (var track in tracklist.OrderBy(p => p.Number))
            {
                if (track.Number != number++)
                {
                    throw new ArgumentException($"Tracks must be numbered in a sequence started at #1.", nameof(tracklist));
                }
            }
        }
    }
}
