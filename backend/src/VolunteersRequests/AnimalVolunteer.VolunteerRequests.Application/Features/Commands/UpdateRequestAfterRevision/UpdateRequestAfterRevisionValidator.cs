using FluentValidation;

namespace AnimalVolunteer.VolunteerRequests.Application.Features.Commands.UpdateRequestAfterRevision;
public class UpdateRequestAfterRevisionValidator : AbstractValidator<UpdateRequestAfterRevisionCommand>
{
    public UpdateRequestAfterRevisionValidator()
    {
        RuleFor(x => x.RequestId).NotEmpty();
    }
}
