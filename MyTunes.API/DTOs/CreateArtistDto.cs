namespace MyTunes.API.DTOs
{
    public class CreateArtistDto
    {
        public CreateArtistDto(string name, string biography)
        {
            Name = name;
            Biography = biography;
        }

        public string Name { get; private set; }
        public string Biography { get; private set; }
    }
}
