using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.DeletePetPhotos;

public class DeletePetPhotosValidator : AbstractValidator<DeletePetPhotosCommand>
{
    public DeletePetPhotosValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty()
            .WithError(Errors.General.InvalidValue(nameof(VolunteerId)));

        RuleFor(x => x.PetId).NotEmpty();
    }
}
