using DomainContactInfo = AnimalVolunteer.Domain.Common.ValueObjects.ContactInfo;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Application.Database;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public class UpdateVolunteerContactInfoHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerContactInfoHandler> _logger;
    private readonly IApplicationDbContext _dbContext;
    public UpdateVolunteerContactInfoHandler(
        IVolunteerRepository volunteerRepository, 
        ILogger<UpdateVolunteerContactInfoHandler> logger,
        IApplicationDbContext dbContext)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, Error>> Update(
        UpdateVolunteerContactInfoComand request, 
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository
            .GetById(request.Id, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var paymentDetails = ContactInfoList.Create(
            request.ContactInfoList.Value.Select(x =>
                DomainContactInfo.Create(x.PhoneNumber, x.Name, x.Note).Value));

        volunteerResult.Value.UpdateContactInfo(paymentDetails);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;
    }
}
