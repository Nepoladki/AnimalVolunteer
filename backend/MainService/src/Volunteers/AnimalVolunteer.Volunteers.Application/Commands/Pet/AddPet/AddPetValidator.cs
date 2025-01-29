using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Pet;
using FluentValidation;
using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Volunteers.Domain.Enums;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.AddPet
{
    public class AddPetValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(Name.MAX_NAME_LENGTH)
                .WithError(Errors.General.InvalidValue(nameof(Name)));

            RuleFor(x => x.Description).MustBeValueObject(Description.Create);

            RuleFor(x => x.Color).NotEmpty()
                .WithError(Errors.General.InvalidValue("Color"));

            RuleFor(x => x.Weight).GreaterThan(0)
                .WithError(Errors.General.InvalidValue("Weight"));

            RuleFor(x => x.Height).GreaterThan(0)
                .WithError(Errors.General.InvalidValue("Height"));

            RuleFor(x => x.SpeciesId).NotEqual(Guid.Empty)
                .WithError(Errors.General.InvalidValue(nameof(SpeciesId)));

            RuleFor(x => x.BreedId).NotEqual(Guid.Empty)
                .WithError(Errors.General.InvalidValue(nameof(BreedId)));

            RuleFor(x => x.HealthDescription).NotEmpty()
                .WithError(Errors.General.InvalidValue("HealthDescription"));

            RuleFor(x => x.Country).NotEmpty().MaximumLength(Address.MAX_LENGTH)
                .WithError(Errors.General.InvalidValue("Country"));

            RuleFor(x => x.City).NotEmpty().MaximumLength(Address.MAX_LENGTH)
                .WithError(Errors.General.InvalidValue("City"));

            RuleFor(x => x.Street).NotEmpty().MaximumLength(Address.MAX_LENGTH)
                .WithError(Errors.General.InvalidValue("Street"));

            RuleFor(x => x.BirthDate).NotEmpty()
                .WithError(Errors.General.InvalidValue("Birthdate"));

            RuleFor(x => x.CurrentStatus).IsInEnum()
                .WithError(Errors.General.InvalidValue(nameof(CurrentStatus)));
        }
    }
}
