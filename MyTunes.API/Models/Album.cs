namespace MyTunes.API.Models
{
    public class Album
    {
        public Album(int id, string name, uint year, IEnumerable<Track> tracklist)
        {
            Id = id;
            Name = name;
            Year = year;
            Tracklist = new List<Track>(tracklist);
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public uint Year { get; private set; }

        public IEnumerable<Track> Tracklist { get; private set; }

        public void Update(string name, uint year, IEnumerable<Track> tracklist)
        {
            Name = name;
            Year = year;
            Tracklist = new List<Track>(tracklist);
        }
    }
}
