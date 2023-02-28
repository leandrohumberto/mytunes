namespace MyTunes.Application.ViewModels.Artist
{
    public class ArtistViewModel
    {
        public ArtistViewModel(int id, string name, string biography)
        {
            Id = id;
            Name = name;
            Biography = biography;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Biography { get; private set; }
    }
}
