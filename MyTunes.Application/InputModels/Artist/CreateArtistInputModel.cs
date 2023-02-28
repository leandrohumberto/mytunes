namespace MyTunes.Application.InputModels.Artist
{
    public class CreateArtistInputModel
    {
        public CreateArtistInputModel(string name, string biography)
        {
            Name = name;
            Biography = biography;
        }

        public string Name { get; private set; }

        public string Biography { get; private set; }
    }
}
