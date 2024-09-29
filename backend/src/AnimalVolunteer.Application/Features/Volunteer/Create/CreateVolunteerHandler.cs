using AnimalVolunteer.Application.Interfaces;
using DomainEntity = AnimalVolunteer.Domain.Aggregates.Volunteer;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using CSharpFunctionalExtensions;
using AnimalVolunteer.Application.Database;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Application.Features.Volunteer.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    public async Task<Result<VolunteerId, Error>> Create(
        CreateVolunteerCommand request,
        CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email).Value;

        if (await _volunteerRepository.ExistByEmail(email, cancellationToken))
            return Errors.Volunteer.AlreadyExist();

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

        var volunteer = DomainEntity.Root.Volunteer.Create(
            VolunteerId.Create(),
            fullName,
            email,
            description,
            statistics,
            contactInfo,
            socialNetworks,
            paymentDetails);

        await _volunteerRepository.Create(volunteer, cancellationToken);
        
        return volunteer.Id;
    }
}
