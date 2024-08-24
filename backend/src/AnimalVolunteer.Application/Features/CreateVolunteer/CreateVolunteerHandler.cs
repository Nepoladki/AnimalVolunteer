using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Aggregates;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.ValueObjects.Common;
using AnimalVolunteer.Domain.ValueObjects.Volunteer;
using CSharpFunctionalExtensions;
using System.ComponentModel;

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

        var volunteerResult = Volunteer.Create(
                fullName,
                request.Description,
                request.ExpirienceYears,
                request.PetsFoundedHome,
                request.PetsLookingForHome,
                request.PetsInVetClinic,
                ContactInfoList.Create(contactList),
                SocialNetworkList.Create(socialNetworksList),
                PaymentDetailsList.Create(paymentDetailsList));

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        await _volunteerRepository.CreateAsync(volunteerResult.Value, cancellationToken);

        return volunteerResult.Value.Id;
    }
}
