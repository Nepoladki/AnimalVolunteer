using AnimalVolunteer.Core;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using AnimalVolunteer.Core.Abstractions.CQRS;
using AnimalVolunteer.SharedKernel;
using AnimalVolunteer.Volunteers.Application.Interfaces;

namespace AnimalVolunteer.Volunteers.Application.Commands.Volunteer.Delete;

public class DeleteVolunteerHandler : ICommandHandler<Guid, DeleteVolunteerCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        [FromKeyedServices(Modules.Volunteers)] IUnitOfWork unitOfWork)
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
