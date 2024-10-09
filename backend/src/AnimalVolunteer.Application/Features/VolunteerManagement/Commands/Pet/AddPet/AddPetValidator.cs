using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Pet;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.AddPet
{
    public class AddPetValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(Name.MAX_NAME_LENGTH)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.Description).NotEmpty()
                .MaximumLength(Description.MAX_DESC_LENGTH)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.Color).NotEmpty()
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.Weight).GreaterThan(0)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.Height).GreaterThan(0)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.SpeciesId).NotEqual(Guid.Empty)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.BreedId).NotEqual(Guid.Empty)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.HealthDescription).NotEmpty()
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.Country).NotEmpty().MaximumLength(Address.MAX_LENGTH)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.City).NotEmpty().MaximumLength(Address.MAX_LENGTH)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.Street).NotEmpty().MaximumLength(Address.MAX_LENGTH)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.BirthDate).NotEmpty()
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.CurrentStatus).IsInEnum();
        }
    }
}
