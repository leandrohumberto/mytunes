namespace MyTunes.API.DTOs
{
    public class UpdateArtistDto
    {
        public UpdateArtistDto(string name, string biography)
        {
            Name = name;
            Biography = biography;
        }

        public string Name { get; private set; }
        public string Biography { get; private set; }
    }
}
