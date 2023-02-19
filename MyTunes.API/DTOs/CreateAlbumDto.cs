using MyTunes.API.Models;

namespace MyTunes.API.DTOs
{
    public class CreateAlbumDto
    {
        public CreateAlbumDto(string name, uint year, IEnumerable<Track> tracklist)
        {
            Name = name;
            Year = year;
            Tracklist = new List<Track>(tracklist);
        }

        public string Name { get; private set; }

        public uint Year { get; private set; }

        public IEnumerable<Track> Tracklist { get; private set; }
    }
}
