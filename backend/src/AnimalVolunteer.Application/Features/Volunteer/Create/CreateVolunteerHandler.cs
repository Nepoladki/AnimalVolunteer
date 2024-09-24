using AnimalVolunteer.Application.Interfaces;
using DomainEntity = AnimalVolunteer.Domain.Aggregates.Volunteer;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using AnimalVolunteer.Application.Database;

namespace AnimalVolunteer.Application.Features.Volunteer.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IApplicationDbContext _dbContext;

    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository, IApplicationDbContext dbContext)
    {
        _volunteerRepository = volunteerRepository;
        _dbContext = dbContext;
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

        var volunteer = DomainEntity.Volunteer.Create(
            VolunteerId.Create(),
            fullName,
            email,
            description,
            statistics,
            contactInfo,
            socialNetworks,
            paymentDetails);

        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }
}
