using MediatR;

namespace MyTunes.Application.Commands.CreateArtist

{
    public class CreateArtistCommand : IRequest<int>
    {
        public CreateArtistCommand(string name, string biography)
        {
            Name = name;
            Biography = biography;
        }

        public string Name { get; private set; }

        public string? Biography { get; private set; }
    }
}
