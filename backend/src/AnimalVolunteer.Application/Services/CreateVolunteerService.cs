using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Application.Requests;
using AnimalVolunteer.Domain.Aggregates;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.ValueObjects.Common;
using AnimalVolunteer.Domain.ValueObjects.Volunteer;
using CSharpFunctionalExtensions;
using System.ComponentModel;

namespace AnimalVolunteer.Application.Services;

public class CreateVolunteerService
{
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerService(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    public async Task<Result<Guid>> Create(CreateVolunteerRequest request)
    {
        var fullNameResult = FullName.Create(request.FirstName, request.SurName, request.LastName);

        if (fullNameResult.IsFailure)
            return Result.Failure<Guid>(fullNameResult.Error);

        if (string.IsNullOrWhiteSpace(request.Description) || request.Description.Length > Constants.TEXT_LENGTH_LIMIT_HIGH)
            return Result.Failure<Guid>("Invalid description");

        var contactList = new List<ContactInfo>();

        foreach (var contact in request.ContactInfoList)
        {
            var infoResult = ContactInfo.Create(contact.PhoneNumber, contact.Name, contact.Note);

            if (infoResult.IsFailure)
                return Result.Failure<Guid>(infoResult.Error);

            contactList.Add(infoResult.Value);
        }

        var socialNetworksList = new List<SocialNetwork>();

        foreach (var network in request.SocialNetworkList)
        {
            var networkResult = SocialNetwork.Create(network.Name, network.URL);

            if (networkResult.IsFailure)
                return Result.Failure<Guid>(networkResult.Error);

            socialNetworksList.Add(networkResult.Value);
        }

        var paymentDetailsList = new List<PaymentDetails>();

        foreach (var payment in request.PaymentDetailsList)
        {
            var paymentResult = PaymentDetails.Create(payment.Name, payment.Descrtiption);

            if (paymentResult.IsFailure)
                return Result.Failure<Guid>(paymentResult.Error);

            paymentDetailsList.Add(paymentResult.Value);
        }

        var newVolunteer = Volunteer.Create(
                fullNameResult.Value,
                request.Description,
                request.ExpirienceYears,
                request.PetsFoundedHome,
                request.PetsLookingForHome,
                request.PetsInVetClinic,
                ContactInfoList.Create(contactList),
                SocialNetworkList.Create(socialNetworksList),
                PaymentDetailsList.Create(paymentDetailsList));

        await _volunteerRepository.CreateAsync(newVolunteer);

        return Result.Success<Guid>(newVolunteer.Id);
    }
}
