using MediatR;

namespace MyTunes.Application.Commands.DeleteArtist
{
    public class DeleteArtistCommand : IRequest<Unit>
    {
        public DeleteArtistCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
