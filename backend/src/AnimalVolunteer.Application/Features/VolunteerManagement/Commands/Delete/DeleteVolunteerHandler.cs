using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Delete;

public class DeleteVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteVolunteerHandler(
        IVolunteerRepository volunteerRepository, IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid, Error>> Delete(
        DeleteVolunteerCommand request,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetById(
            request.Id,
            cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        volunteerResult.Value.Delete();

        await _unitOfWork.SaveChanges(cancellationToken);

        return (Guid)volunteerResult.Value.Id;
    }
}
