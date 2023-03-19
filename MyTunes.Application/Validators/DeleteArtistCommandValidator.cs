using FluentValidation;
using MyTunes.Application.Commands.DeleteAlbum;

namespace MyTunes.Application.Validators
{
    public class DeleteArtistCommandValidator : AbstractValidator<DeleteAlbumCommand>
    {
        public DeleteArtistCommandValidator()
        {
            RuleFor(a => a.Id)
                .GreaterThan(0);
        }
    }
}
