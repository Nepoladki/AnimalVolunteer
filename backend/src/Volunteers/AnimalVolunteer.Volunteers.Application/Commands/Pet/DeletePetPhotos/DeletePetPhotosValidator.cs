using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.DeletePetPhotos;

public class DeletePetPhotosValidator : AbstractValidator<DeletePetPhotosCommand>
{
    public DeletePetPhotosValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty();

        RuleFor(x => x.PetId).NotEmpty();
    }
}
