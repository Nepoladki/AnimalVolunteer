using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Features.Volunteer.Delete;

public class DeleteVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    public DeleteVolunteerHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    public async Task<Result<Guid, Error>> Delete(
        DeleteVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var volunteer = await _volunteerRepository.GetById(
            request.Id,
            cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(request.Id);

        volunteer.Delete();

        await _volunteerRepository.Save(volunteer, cancellationToken);

        return (Guid)volunteer.Id;
    }
}
