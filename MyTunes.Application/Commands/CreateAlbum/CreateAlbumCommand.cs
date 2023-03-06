using MediatR;
using MyTunes.Application.InputModels.Track;
using MyTunes.Core.Enums;

namespace MyTunes.Application.Commands
{
    public class CreateAlbumCommand : IRequest<int>
    {
        public CreateAlbumCommand(string name, int idArtist, uint year, string genre, AlbumFormat format, IEnumerable<TrackInputModel> tracklist)
        {
            Name = name;
            IdArtist = idArtist;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = new List<TrackInputModel>(tracklist);
        }

        public string Name { get; private set; }

        public int IdArtist { get; private set; }

        public uint Year { get; private set; }

        public string Genre { get; private set; }

        public AlbumFormat Format { get; private set; }

        public IEnumerable<TrackInputModel> Tracklist { get; private set; }
    }
}
