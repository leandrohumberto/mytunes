namespace MyTunes.Core.Entities
{
    public class Artist : BaseEntity
    {
        public Artist(string name, string biography)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            Name = name;
            Biography = biography;
            Albums = new List<Album>();
        }

        public string Name { get; private set; }

        public string Biography { get; private set; }

        public List<Album> Albums { get; private set; }

        public void Update(string name, string biography)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            Name = name;
            Biography = biography;
        }
    }
}
