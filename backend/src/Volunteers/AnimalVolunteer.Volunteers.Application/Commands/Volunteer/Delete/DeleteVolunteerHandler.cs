using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Volunteers.Application;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Delete;

public class DeleteVolunteerHandler : ICommandHandler<Guid, DeleteVolunteerCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteVolunteerHandler(
        IVolunteerRepository volunteerRepository, IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteVolunteerCommand request,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetById(
            request.Id,
            cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        volunteerResult.Value.SoftDelete();

        await _unitOfWork.SaveChanges(cancellationToken);

        return (Guid)volunteerResult.Value.Id;
    }
}
