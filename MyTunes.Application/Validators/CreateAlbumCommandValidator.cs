using FluentValidation;
using MyTunes.Application.Commands;

namespace MyTunes.Application.Validators
{
    public class CreateAlbumCommandValidator : AbstractValidator<CreateAlbumCommand>
    {
        public CreateAlbumCommandValidator()
        {
            RuleFor(a => a.Title)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.IdArtist)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(a => a.Genre)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Format)
                .IsInEnum();

            RuleFor(a => a.Tracklist)
                .NotEmpty();

            RuleFor(a => a.Tracklist)
                .ForEach(a => a.SetValidator(new TrackInputModelValidator()));

            RuleFor(a => a.Tracklist)
                .Must(tracklist =>
                {
                    var number = 1;

                    foreach (var track in tracklist.OrderBy(p => p.Number))
                    {
                        if (track.Number != number++)
                        {
                            return false;
                        }
                    }

                    return true;
                })
                .WithMessage("Tracks must be numbered in a sequence started at #1.");
        }
    }
}
