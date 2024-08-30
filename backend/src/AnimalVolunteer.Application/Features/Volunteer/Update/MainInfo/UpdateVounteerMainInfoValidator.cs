using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common.ValueObjects;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;

public class UpdateVounteerMainInfoValidator : AbstractValidator<UpdateVounteerMainInfoRequest>
{
    public UpdateVounteerMainInfoValidator()
    {
        RuleFor(r => r.Dto.FullName)
            .MustBeValueObject(dto => FullName.Create(dto.FirstName, dto.SurName, dto.LastName));

        RuleFor(r => r.Dto.Email).MustBeValueObject(Email.Create);

        RuleFor(r => r.Dto.Description).MustBeValueObject(Description.Create);
    }
};
