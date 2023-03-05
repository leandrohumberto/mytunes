namespace MyTunes.Core.Entities
{
    public class Track : BaseEntity
    {
        public Track(uint number, string name, TimeSpan length)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (number <= 0)
            {
                throw new ArgumentException($"'{nameof(number)}' cannot be zero.", nameof(name));
            }

            if (length.TotalSeconds <= 0.0)
            {
                throw new ArgumentException($"'{nameof(length)}' total seconds cannot be less than or equals to zero.", nameof(length));
            }

            Number = number;
            Name = name;
            Length = length;
        }

        public int IdAlbum { get; private set; }

        public Album? Album { get; private set; }

        public uint Number { get; private set; }

        public string Name { get; private set; }

        public TimeSpan Length { get; private set; }
    }
}
