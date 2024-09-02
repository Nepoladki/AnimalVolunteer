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
    public UpdateVolunteerSocialNetworksHandler(IVolunteerRepository volunteerRepository, ILogger<UpdateVolunteerSocialNetworksHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Update(UpdateVolunteerSocialNetworksRequest request, CancellationToken cancellationToken)
    {
        var volunteer = await _volunteerRepository.GetById(request.Id, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(request.Id);

        var socialNetworks = SocialNetworkList.Create(
            request.SocialNetworksList.Value.Select(x => SocialNetwork.Create(x.Name, x.URL).Value));

        volunteer.UpdateSocialNetworks(socialNetworks);

        await _volunteerRepository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteer.Id);

        return (Guid)volunteer.Id;
    }
}
