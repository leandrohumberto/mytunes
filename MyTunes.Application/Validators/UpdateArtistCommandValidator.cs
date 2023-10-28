using FluentValidation;
using MyTunes.Application.Commands.UpdateArtist;

namespace MyTunes.Application.Validators
{
    public class UpdateArtistCommandValidator : AbstractValidator<UpdateArtistCommand>
    {
        public UpdateArtistCommandValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}
