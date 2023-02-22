namespace MyTunes.Core.Entities
{
    public class Track : BaseEntity
    {
        public Track(uint number, string name, TimeSpan length)
        {
            Number = number;
            Name = name;
            Length = length;
        }

        public uint Number { get; private set; }

        public string Name { get; private set; }

        public TimeSpan Length { get; private set; }
    }
}
