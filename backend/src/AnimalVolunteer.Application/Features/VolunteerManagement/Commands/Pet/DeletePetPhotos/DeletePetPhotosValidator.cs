using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.DeletePetPhotos;

public class DeletePetPhotosValidator : AbstractValidator<DeletePetPhotosCommand>
{
    public DeletePetPhotosValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty();

        RuleFor(x => x.PetId).NotEmpty();
    }
}
