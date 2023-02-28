using MyTunes.Core.Entities;
using MyTunes.Core.Enums;

namespace MyTunes.Infrastructure.Persistence
{
    public class MyTunesDbContext
    {
        public MyTunesDbContext()
        {
            Artists = new Dictionary<int, Artist>();
            Albums = new Dictionary<int, Album>();
            Users = new Dictionary<int, User>();
            InitializeSampleData();
        }

        public Dictionary<int, Artist> Artists { get; private set; }

        public Dictionary<int, Album> Albums { get; private set; }

        public Dictionary<int, User> Users { get; private set; }

        private void InitializeSampleData()
        {
            var ghost = new Artist(
                "Ghost",
                "Melding surprisingly accessible metal- and pop-driven hard rock, costumed Swedish outfit Ghost deliver sonic sermons centered on horror imagery, the occult, and Satanic themes. The group is fronted by lead singer, songwriter, and conceptualist Tobias Forge, who dons various demonic Pope costumes and is backed by a band of \"Nameless Ghouls\" hidden in cloaks and heavy makeup.");

            Artists = new Dictionary<int, Artist>
            {
                { 1, ghost },
            };

            var impera = new Album("Impera", 1, 2022, "Metal", AlbumFormat.FullLength,
                new List<Track>
                {
                    new Track(1, "Imperium", new TimeSpan(0, 1, 40)),
                    new Track(2, "Kaisarion", new TimeSpan(0, 5, 2)),
                    new Track(3, "Spillways", new TimeSpan(0, 3, 16)),
                    new Track(4, "Call Me Little Sunshine", new TimeSpan(0, 4, 44)),
                    new Track(5, "Hunter's Moon", new TimeSpan(0, 3, 16)),
                    new Track(6, "Watcher in the Sky", new TimeSpan(0, 5, 48)),
                    new Track(7, "Dominion", new TimeSpan(0, 1, 22)),
                    new Track(8, "Twenties", new TimeSpan(0, 3, 46)),
                    new Track(9, "Darkness at the Heart of My Love", new TimeSpan(0, 4, 58)),
                    new Track(10, "Griftwood", new TimeSpan(0, 5, 16)),
                    new Track(11, "Bite of Passage", new TimeSpan(0, 0, 31)),
                    new Track(12, "Respite on the Spitalfields", new TimeSpan(0, 6, 42)),
                });

            var huntersMoon = new Album("Hunter's Moon", 1, 2022, "Metal", AlbumFormat.Single,
                new List<Track>
                {
                    new Track(1, "Hunter's Moon", new TimeSpan(0, 3, 17)),
                    new Track(2, "Halloween Kills (Main Title)", new TimeSpan(0, 1, 46)),
                });


            Albums = new Dictionary<int, Album>
            {
                { 1, impera },
                { 2, huntersMoon },
            };

            ghost.Albums.Add(impera);
            ghost.Albums.Add(huntersMoon);

            var metallica = new Artist(
                "Metallica",
                "Metallica is one of the most important and most influential metal bands of all time, drafting the blueprint for thrash metal in their earliest days, then pushing the boundaries of mainstream metal and hard rock as they settled into their role of a popular legacy act as the decades went on.");

            Artists.Add(2, metallica);

            var blackAlbum = new Album("Metallica", 2, 1991, "Metal", AlbumFormat.FullLength,
                new List<Track>
                {
                    new Track(1, "Enter Sandman", new TimeSpan(0, 5, 31)),
                    new Track(2, "Sad but True", new TimeSpan(0, 5, 24)),
                    new Track(3, "Holier than Thou", new TimeSpan(0, 3, 47)),
                    new Track(4, "The Unforgiven", new TimeSpan(0, 6, 27)),
                    new Track(5, "Wherever I May Roam", new TimeSpan(0, 6, 44)),
                    new Track(6, "Don't Tread on Me", new TimeSpan(0, 4, 0)),
                    new Track(7, "Through the Never", new TimeSpan(0, 4, 4)),
                    new Track(8, "Nothing Else Matters", new TimeSpan(0, 6, 28)),
                    new Track(9, "Of Wolf and Man", new TimeSpan(0, 4, 16)),
                    new Track(10, "The God That Failed", new TimeSpan(0, 5, 8)),
                    new Track(11, "My Friend of Misery", new TimeSpan(0, 6, 49)),
                    new Track(12, "The Struggle Within", new TimeSpan(0, 3, 53)),
                });

            var enterSandman = new Album("Enter Sandman", 2, 1991, "Metal", AlbumFormat.Single,
                new List<Track>
                {
                    new Track(1, "Enter Sandman", new TimeSpan(0, 5, 37)),
                    new Track(1, "Stone Cold Crazy", new TimeSpan(0, 2, 19)),
                });

            Albums.Add(3, blackAlbum);
            Albums.Add(4, enterSandman);
            metallica.Albums.Add(blackAlbum);
            metallica.Albums.Add(enterSandman);
        }
    }
}
