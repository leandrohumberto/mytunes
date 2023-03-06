using MediatR;

namespace MyTunes.Application.Commands.DeleteAlbum
{
    public class DeleteAlbumCommand : IRequest<Unit>
    {
        public DeleteAlbumCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
