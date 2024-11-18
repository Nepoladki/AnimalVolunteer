using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.Discussions.Domain.Aggregate.ValueObjects;
using FluentValidation;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.AddMessage;
public class AddMessageValidator : AbstractValidator<AddMessageCommand>
{
    public AddMessageValidator()
    {
        RuleFor(x => x.Text).MustBeValueObject(Text.Create);
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.RelatedId).NotEmpty();
        RuleFor(x => x.RelatedId).NotEmpty();
    }
}
