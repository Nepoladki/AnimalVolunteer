using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel;
using FluentValidation;

namespace AnimalVolunteer.Accounts.Application.Commands.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress()
            .WithError(Errors.General.InvalidValue("Email"));

        RuleFor(x => x.UserName).NotEmpty().MinimumLength(6).MaximumLength(24)
            .WithError(Errors.General.InvalidValue("Username"));

        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(24)
            .WithError(Errors.General.InvalidValue("Password"));

        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(2).MaximumLength(24)
            .WithError(Errors.General.InvalidValue("FirstName"));

        RuleFor(x => x.LastName).NotEmpty().MinimumLength(2).MaximumLength(24)
            .WithError(Errors.General.InvalidValue("LastName"));
    }
}
