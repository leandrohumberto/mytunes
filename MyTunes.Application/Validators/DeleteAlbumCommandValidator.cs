using FluentValidation;
using MyTunes.Application.Commands.DeleteArtist;

namespace MyTunes.Application.Validators
{
    public class DeleteAlbumCommandValidator : AbstractValidator<DeleteArtistCommand>
    {
        public DeleteAlbumCommandValidator()
        {
            RuleFor(a => a.Id)
                .GreaterThan(0);
        }
    }
}
