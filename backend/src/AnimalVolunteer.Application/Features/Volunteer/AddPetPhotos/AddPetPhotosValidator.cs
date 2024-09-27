using AnimalVolunteer.Application.DTOs.Validators;
using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Common;
using FluentValidation;

namespace AnimalVolunteer.Application.Features.Volunteer.AddPetPhotos
{
    public class AddPetPhotosValidator : AbstractValidator<AddPetPhotosCommand>
    {
        public AddPetPhotosValidator()
        {
            RuleFor(x => x.VolunteerId).NotEmpty().NotEqual(Guid.Empty)
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.PetId).NotEmpty().NotEqual(Guid.Empty)
                .WithError(Errors.General.InvalidValue());

            RuleForEach(x => x.Files).SetValidator(new UploadFileDtoValidator());
        }
    }
}
