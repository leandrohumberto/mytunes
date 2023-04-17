using FluentValidation;
using MyTunes.Application.Commands.LoginUser;

namespace MyTunes.Application.Validators
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
