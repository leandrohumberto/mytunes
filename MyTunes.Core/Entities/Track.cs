namespace MyTunes.Core.Entities
{
    public class Track : BaseEntity
    {
        public Track(uint number, string title, TimeSpan length)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentException($"'{nameof(title)}' cannot be null or empty.", nameof(title));
            }

            if (number <= 0)
            {
                throw new ArgumentException($"'{nameof(number)}' cannot be zero.", nameof(title));
            }

            if (length.TotalSeconds <= 0.0)
            {
                throw new ArgumentException($"'{nameof(length)}' total seconds cannot be less than or equals to zero.", nameof(length));
            }

            Number = number;
            Title = title;
            Length = length;
        }

        public int IdAlbum { get; private set; }

        public Album? Album { get; private set; }

        public uint Number { get; private set; }

        public string Title { get; private set; }

        public TimeSpan Length { get; private set; }
    }
}
