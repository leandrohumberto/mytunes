namespace MyTunes.Application.InputModels.Artist
{
    public class UpdateArtistInputModel
    {
        public UpdateArtistInputModel(string name, string biography)
        {
            Name = name;
            Biography = biography;
        }

        public string Name { get; private set; }

        public string Biography { get; private set; }
    }
}
