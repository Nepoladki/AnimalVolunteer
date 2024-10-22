using FluentValidation;

namespace AnimalVolunteer.Accounts.Application.Commands.RegisterUser;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.UserName).NotEmpty().MinimumLength(6).MaximumLength(24);
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(24);
    }
}
