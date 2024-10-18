using AnimalVolunteer.Core.DTOs.Validators;
using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel;
using FluentValidation;

namespace AnimalVolunteer.Volunteers.Application.Commands.Pet.UpdatePetPhotos
{
    public class UpdatePetPhotosValidator : AbstractValidator<UpdatePetPhotosCommand>
    {
        public UpdatePetPhotosValidator()
        {
            RuleFor(x => x.VolunteerId).NotEmpty().NotEqual(Guid.Empty)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.PetId).NotEmpty().NotEqual(Guid.Empty)
                .WithError(Errors.General.InvalidValue());

            RuleForEach(x => x.Files).SetValidator(new UploadFileDtoValidator());
        }
    }
}
