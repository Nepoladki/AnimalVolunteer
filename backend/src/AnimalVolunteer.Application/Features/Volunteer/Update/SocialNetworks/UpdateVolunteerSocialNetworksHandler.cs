using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.SocialNetworks;

public class UpdateVolunteerSocialNetworksHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerSocialNetworksHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateVolunteerSocialNetworksHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerSocialNetworksHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
}

    public async Task<Result<Guid, Error>> Update(
        UpdateVolunteerSocialNetworksCommand request, 
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository
            .GetById(request.Id, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var socialNetworks = SocialNetworkList.Create(
            request.SocialNetworksList.Value
            .Select(x => SocialNetwork.Create(x.Name, x.URL).Value));

        volunteerResult.Value.UpdateSocialNetworks(socialNetworks);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;
    }
}
