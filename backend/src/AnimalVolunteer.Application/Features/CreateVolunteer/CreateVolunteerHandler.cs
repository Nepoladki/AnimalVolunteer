using AnimalVolunteer.Application.Interfaces;
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
    public async Task<Result<VolunteerId, Error>> Create(CreateVolunteerRequest request, CancellationToken cancellationToken)
    {
        var fullName = FullName.Create(request.FirstName, request.SurName, request.LastName).Value;

        var description = Description.Create(request.Description);

        var statistics = Statistics.Create(
            request.ExpirienceYears,
            request.PetsFoundedHome,
            request.PetsLookingForHome,
            request.PetsInVetClinic);

        if (string.IsNullOrWhiteSpace(request.Description) || request.Description.Length > Constants.TEXT_LENGTH_LIMIT_HIGH)
            return Errors.General.InvalidValue(nameof(request.Description));

        var contactList = new List<ContactInfo>();

        foreach (var contact in request.ContactInfoList)
        {
            var infoResult = ContactInfo.Create(contact.PhoneNumber, contact.Name, contact.Note);

            if (infoResult.IsFailure)
                return infoResult.Error;

            contactList.Add(infoResult.Value);
        }

        var socialNetworksList = new List<SocialNetwork>();

        foreach (var network in request.SocialNetworkList)
        {
            var networkResult = SocialNetwork.Create(network.Name, network.URL);

            if (networkResult.IsFailure)
                return networkResult.Error;

            socialNetworksList.Add(networkResult.Value);
        }

        var paymentDetailsList = new List<PaymentDetails>();

        foreach (var payment in request.PaymentDetailsList)
        {
            var paymentResult = PaymentDetails.Create(payment.Name, payment.Descrtiption);

            if (paymentResult.IsFailure)
                return paymentResult.Error;

            paymentDetailsList.Add(paymentResult.Value);
        }

        var volunteer = Volunteer.CreateWithLists(
            VolunteerId.Create(),
            fullName,
            description.Value,
            statistics.Value,
            ContactInfoList.Create(contactList),
            SocialNetworkList.Create(socialNetworksList),
            PaymentDetailsList.Create(paymentDetailsList));

        await _volunteerRepository.CreateAsync(volunteer, cancellationToken);

        return volunteer.Id;
    }
}
