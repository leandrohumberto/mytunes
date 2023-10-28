using MediatR;
using MyTunes.Application.InputModels.Track;
using MyTunes.Core.Enums;

namespace MyTunes.Application.Commands.UpdateAlbum
{
    public class UpdateAlbumCommand : IRequest<Unit>
    {
        public UpdateAlbumCommand(string title, uint year, string genre, AlbumFormat format, IEnumerable<TrackInputModel> tracklist)
        {
            Title = title;
            Year = year;
            Genre = genre;
            Format = format;
            Tracklist = tracklist != null ? new List<TrackInputModel>(tracklist) : new List<TrackInputModel>();
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public uint Year { get; private set; }

        public string Genre { get; private set; }

        public AlbumFormat Format { get; private set; }

        public IEnumerable<TrackInputModel> Tracklist { get; private set; }

        public void SetId(int id) => Id = id;
    }
}
