﻿using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates.Volunteer;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;

namespace AnimalVolunteer.Application.Features.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    public async Task<Result<VolunteerId, Error>> Create(
        CreateVolunteerRequest request, 
        CancellationToken cancellationToken)
    {
        var fullName = FullName.Create(
            request.FullName.FirstName, 
            request.FullName.SurName, 
            request.FullName.LastName).Value;

        var description = Description.Create(request.Description).Value;

        var statistics = Statistics.CreateEmpty();

        var contactInfo = ContactInfoList.CreateEmpty();

        var socialNetworks = SocialNetworkList.Create(request.SocialNetworkList
            .Select(x => SocialNetwork.Create(x.Name, x.URL).Value));

        var paymentDetails = PaymentDetailsList.Create(request.PaymentDetailsList
            .Select(x => PaymentDetails.Create(x.Name, x.Description).Value));

        var volunteer = Volunteer.Create(
            VolunteerId.Create(),
            fullName,
            description,
            statistics,
            contactInfo,
            socialNetworks,
            paymentDetails);

        await _volunteerRepository.CreateAsync(volunteer, cancellationToken);

        return volunteer.Id;
    }
}
