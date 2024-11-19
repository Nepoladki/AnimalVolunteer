using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.Discussions.Domain.Aggregate.ValueObjects;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AnimalVolunteer.Discussions.Application.Features.Commands.AmendMessage;

public class AmendMessageValidator : AbstractValidator<AmendMessageCommand>
{
    public AmendMessageValidator()
    {
        RuleFor(x => x.NewText).MustBeValueObject(Text.Create);
        RuleFor(x => x.DiscussionId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.MessageId).NotEmpty();
    }
}

