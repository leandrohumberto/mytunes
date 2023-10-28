using FluentValidation;
using MyTunes.Application.Commands.CreateUser;
using System.Text.RegularExpressions;

namespace MyTunes.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(u => u.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);

            RuleFor(u => u.Role)
                .IsInEnum();

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull();

            RuleFor(u => u.Password)
                .Must(ValidatePassword)
                .WithMessage("Password must be at least 8-character long, and contain a number, " +
                    "a lower case letter, uppercase letter, and a special character.");
        }

        private bool ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            return regex.IsMatch(password);
        }
    }
}
