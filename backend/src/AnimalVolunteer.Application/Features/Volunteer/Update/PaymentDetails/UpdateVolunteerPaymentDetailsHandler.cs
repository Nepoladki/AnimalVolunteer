using DomainPaymentDetails = AnimalVolunteer.Domain.Common.ValueObjects.PaymentDetails;
using AnimalVolunteer.Application.Interfaces;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using AnimalVolunteer.Application.Database;

namespace AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;

public class UpdateVolunteerPaymentDetailsHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerPaymentDetailsHandler> _logger;
    private readonly IApplicationDbContext _dbContext;
    public UpdateVolunteerPaymentDetailsHandler
        (IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerPaymentDetailsHandler> logger,
        IApplicationDbContext dbContext)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, Error>> Update(
        UpdateVolunteerPaymentDetailsCommand request, 
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository
            .GetById(request.Id, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var paymentDetails = PaymentDetailsList.Create(
            request.PaymentDetailsList.Value.Select(x => 
                DomainPaymentDetails.Create(x.Name, x.Description).Value));

        volunteerResult.Value.UpdatePaymentDetails(paymentDetails);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Volunteer {ID} updated", volunteerResult.Value.Id);

        return (Guid)volunteerResult.Value.Id;
    }
}
