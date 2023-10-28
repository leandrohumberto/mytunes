using MyTunes.Core.Enums;

namespace MyTunes.Core.Entities
{
    public class Album : BaseEntity
    {
        public Album(string title, int idArtist, uint year, string genre, AlbumFormat format)
        {
            Validate(title, year, genre);
            Title = title;
            IdArtist = idArtist;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = new List<Track>();

        }

        public Album(string title, int idArtist, uint year, string genre, AlbumFormat format, IEnumerable<Track> tracklist)
            : this(title, idArtist, year, genre, format)
        {
            Validate(tracklist);
            Tracklist = new List<Track>(tracklist);
        }

        public string Title { get; private set; }

        public int IdArtist { get; private set; }

        public Artist? Artist { get; private set; }

        public uint Year { get; private set; }

        public string Genre { get; private set; }

        public AlbumFormat Format { get; private set; }

        public List<Track> Tracklist { get; private set; }

        public void Update(string title, uint year, string genre, AlbumFormat format, IEnumerable<Track> tracklist)
        {
            Validate(title, year, genre);
            Validate(tracklist);

            Title = title;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = new List<Track>(tracklist);
        }

        private static void Validate(string title, uint year, string genre)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or empty.", nameof(title));
            }

            if (year == 0)
            {
                throw new ArgumentException($"'{nameof(year)}' cannot be zero.", nameof(title));
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
