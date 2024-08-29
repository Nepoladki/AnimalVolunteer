using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;

public class UpdateVounteerMainInfoHandler
{
    private readonly ILogger<UpdateVounteerMainInfoHandler> _logger;
    private readonly IVolunteerRepository _volunteerRepository;

    public UpdateVounteerMainInfoHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVounteerMainInfoHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }
    public async Task<Result<Guid, Error>> Update(UpdateVounteerMainInfoRequest request, CancellationToken cancellationToken)
    {
        var volunteer = await _volunteerRepository.GetByID(request.Id, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(request.Id);

        var fullName = FullName.Create(
            request.Dto.FullName.FirstName,
            request.Dto.FullName.SurName,
            request.Dto.FullName.LastName).Value;

        var email = Email.Create(request.Dto.Email).Value;

        var description = Description.Create(request.Dto.Description).Value;

        volunteer.UpdateMainInfo(fullName, email, description);

        await _volunteerRepository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteer.Id);

        return (Guid)volunteer.Id;
    }
};
