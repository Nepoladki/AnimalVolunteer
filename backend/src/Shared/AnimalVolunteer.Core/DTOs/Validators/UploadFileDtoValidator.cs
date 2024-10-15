using AnimalVolunteer.Core.DTOs.VolunteerManagement.Pet;
using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.Domain.Common;
using FluentValidation;

namespace AnimalVolunteer.Core.DTOs.Validators
{
    public class UploadFileDtoValidator : AbstractValidator<UploadFileDto>
    {
        public UploadFileDtoValidator()
        {
            RuleFor(x => x.FileName).NotEmpty()
                .WithError(Errors.General.InvalidValue());

            RuleFor(x => x.Content.Length).LessThan(5_000_000);
        }
    }
}
