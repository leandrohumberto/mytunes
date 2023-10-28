using FluentValidation;
using MyTunes.Application.Commands.UpdateAlbum;

namespace MyTunes.Application.Validators
{
    public class UpdateAlbumCommandValidator : AbstractValidator<UpdateAlbumCommand>
    {
        public UpdateAlbumCommandValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Genre)
                .NotEmpty()
                .NotNull();

            RuleFor(a => a.Format)
                .IsInEnum();

            RuleFor(a => a.Tracklist)
                .NotEmpty();

            RuleFor(a => a.Tracklist)
                .ForEach(t => t.SetValidator(new TrackInputModelValidator()));

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
