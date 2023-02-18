namespace MyTunes.API.Models
{
    public class Artist
    {
        public Artist(int id, string name, string biography)
        {
            Id = id;
            Name = name;
            Biography = biography;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Biography { get; private set; }

        public void Update(string name, string biography)
        {
            Name = name;
            Biography = biography;
        }
    }
}
