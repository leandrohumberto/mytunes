namespace MyTunes.Core.Entities
{
    public class Artist : BaseEntity
    {
        public Artist(string name, string biography, IEnumerable<Album> albums)
        {
            Name = name;
            Biography = biography;
            Albums = new List<Album>(albums);
        }

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
