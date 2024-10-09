using AnimalVolunteer.Application.DTOs.Validators;
using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Common;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.AddPetPhotos
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
