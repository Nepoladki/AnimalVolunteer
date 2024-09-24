using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Features.Volunteer.Delete;

public class DeleteVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IApplicationDbContext _dbContext;
    public DeleteVolunteerHandler(
        IVolunteerRepository volunteerRepository, IApplicationDbContext dbContext)
    {
        _volunteerRepository = volunteerRepository;
        _dbContext = dbContext;
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

        await _dbContext.SaveChangesAsync(cancellationToken);

        return (Guid)volunteerResult.Value.Id;
    }
}
