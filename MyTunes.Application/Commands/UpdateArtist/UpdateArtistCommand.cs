using MediatR;

namespace MyTunes.Application.Commands.UpdateArtist
{
    public class UpdateArtistCommand : IRequest<Unit>
    {
        public UpdateArtistCommand(string name, string biography)
        {
            Name = name;
            Biography = biography;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string? Biography { get; private set; }

        public void SetId(int id) => Id = id;
    }
}
