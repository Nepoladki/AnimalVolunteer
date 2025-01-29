using AnimalVolunteer.Core.Validation;
using AnimalVolunteer.SharedKernel.ValueObjects;
using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.UpdateRequest;
public class UpdateRequestValidator : AbstractValidator<UpdateRequestCommand>
{
    public UpdateRequestValidator()
    {
        RuleFor(x => x.RequestId).NotEmpty();

        RuleFor(x => x.VolunteerInfo)
            .MustBeValueObject(vi => VolunteerInfo.Create(
                vi.ExpirienceDescription, 
                vi.Passport));
    }
}
