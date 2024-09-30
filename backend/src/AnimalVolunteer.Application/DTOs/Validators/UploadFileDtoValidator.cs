using AnimalVolunteer.Application.DTOs.Volunteer.Pet;
using AnimalVolunteer.Application.Validation;
using AnimalVolunteer.Domain.Common;
using FluentValidation;

namespace AnimalVolunteer.Application.DTOs.Validators
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
