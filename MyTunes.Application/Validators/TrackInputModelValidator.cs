using FluentValidation;
using MyTunes.Application.InputModels.Track;

namespace MyTunes.Application.Validators
{
    public class TrackInputModelValidator : AbstractValidator<TrackInputModel>
    {
        public TrackInputModelValidator()
        {
            RuleFor(t => t.Name)
                    .NotEmpty();

            RuleFor(t => t.Number)
                .GreaterThan(0u);

            RuleFor(t => t.Length)
                .GreaterThan(TimeSpan.FromSeconds(0));
        }
    }
}
