using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using FluentValidation;

namespace AnimalVolunteer.Accounts.Application.Commands.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress()
            .WithError(Errors.General.InvalidValue("Email"));

        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(24)
            .WithError(Errors.General.InvalidValue("Password"));

        RuleFor(x => x.FullName)
            .MustBeValueObject(fn => FullName.Create(fn.FirstName, fn.Patronymic, fn.LastName));

    }
}
