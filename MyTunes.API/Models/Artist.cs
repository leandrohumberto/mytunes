namespace MyTunes.API.Models
{
    public class Artist
    {
        public Artist(int id, string name, string biography, IEnumerable<Album> albums)
        {
            Id = id;
            Name = name;
            Biography = biography;
            Albums = new List<Album>(albums);
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Biography { get; private set; }

        public IEnumerable<Album> Albums { get; private set; }

        public void Update(string name, string biography, IEnumerable<Album> albums)
        {
            Name = name;
            Biography = biography;
            Albums = new List<Album>(albums);
        }
    }
}
