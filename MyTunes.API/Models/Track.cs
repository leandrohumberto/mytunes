namespace MyTunes.API.Models
{
    public class Track
    {
        public Track(int id, uint number, string name)
        {
            Id = id;
            Number = number;
            Name = name;
        }

        public int Id { get; private set; }

        public uint Number { get; private set; }

        public string Name { get; private set; }
    }
}
