using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.Domain.Common.ValueObjects;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Volunteers.Domain.Enums;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Pet;
using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.UpdatePet;

public class UpdatePetValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(Name.MAX_NAME_LENGTH)
                .WithError(Errors.General.InvalidValue());

        RuleFor(x => x.Description).NotEmpty()
            .MaximumLength(Description.MAX_DESC_LENGTH)
            .WithError(Errors.General.InvalidValue(nameof(Description)));

        RuleFor(x => x.Color).NotEmpty()
            .WithError(Errors.General.InvalidValue("Color"));

        RuleFor(x => x.Weight).GreaterThan(0)
            .WithError(Errors.General.InvalidValue("Weight"));

        RuleFor(x => x.Height).GreaterThan(0)
            .WithError(Errors.General.InvalidValue("Height"));

        RuleFor(x => x.SpeciesId).NotEqual(Guid.Empty)
            .WithError(Errors.General.InvalidValue(nameof(SpeciesId)));

        RuleFor(x => x.BreedId).NotEqual(Guid.Empty)
            .WithError(Errors.General.InvalidValue());

        RuleFor(x => x.HealthDescription).NotEmpty()
            .WithError(Errors.General.InvalidValue("HealthDescription"));

        RuleFor(x => x.Country).NotEmpty().MaximumLength(Address.MAX_LENGTH)
            .WithError(Errors.General.InvalidValue("Country"));

        RuleFor(x => x.City).NotEmpty().MaximumLength(Address.MAX_LENGTH)
            .WithError(Errors.General.InvalidValue("City"));

        RuleFor(x => x.Street).NotEmpty().MaximumLength(Address.MAX_LENGTH)
            .WithError(Errors.General.InvalidValue("Street"));

        RuleFor(x => x.BirthDate).NotEmpty()
            .WithError(Errors.General.InvalidValue("BirthDate"));

        RuleFor(x => x.CurrentStatus).IsInEnum()
            .WithError(Errors.General.InvalidValue(nameof(CurrentStatus)));

        RuleForEach(r => r.ContactInfo).ChildRules(contactInfo =>
        {
            contactInfo.RuleFor(x => new { x.PhoneNumber, x.Name, x.Note })
                .MustBeValueObject(z => ContactInfo.Create(
                    z.PhoneNumber, z.Name, z.Note));
        });

        RuleForEach(r => r.PaymentDetails).ChildRules(paymentDetails =>
        {
            paymentDetails.RuleFor(x => new { x.Name, x.Description })
                .MustBeValueObject(z => PaymentDetails.Create(z.Name, z.Description));
        });
    }
}
