using FluentValidation;
using MyTunes.Application.Commands.CreateArtist;

namespace MyTunes.Application.Validators
{
    public class CreateArtistCommandValidator : AbstractValidator<CreateArtistCommand>
    {
        public CreateArtistCommandValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}
