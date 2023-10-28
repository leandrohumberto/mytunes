namespace MyTunes.Core.Exceptions
{
    public class ArtistNotFoundException : Exception
    {
        public ArtistNotFoundException(int id, string? message = default) : base(message)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
