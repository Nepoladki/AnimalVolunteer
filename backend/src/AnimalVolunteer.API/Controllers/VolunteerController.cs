using AnimalVolunteer.API.Extensions;
using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.Volunteer.CreateVolunteer;
using AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;
using AnimalVolunteer.Application.Features.Volunteer.Update.SocialNetworks;
using AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.API.Controllers;
public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateVolunteerRequest request,
        [FromServices] CreateVolunteerHandler handler,
        CancellationToken cancellationToken = default)
    {
        var creationResult = await handler.Create(request, cancellationToken);

        if (creationResult.IsFailure)
            return creationResult.Error.ToResponse();

        return Ok((Guid)creationResult.Value);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] MainInfoDto mainInfo,
        [FromServices] UpdateVounteerMainInfoHandler handler,
        [FromServices] IValidator<UpdateVounteerMainInfoRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateVounteerMainInfoRequest(id, mainInfo);

        var validatonResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validatonResult.IsValid)
            return validatonResult.ToValidationErrorResponse();

        var handleResult = await handler.Update(request, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<IActionResult> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromBody] SocialNetworksListDto socialNetworks,
        [FromServices] UpdateVolunteerSocialNetworksHandler handler,
        [FromServices] IValidator<UpdateVolunteerSocialNetworksRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateVolunteerSocialNetworksRequest(id, socialNetworks);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var handleResult = await handler.Update(request, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpPut("{id:guid}/payment-details")]
    public async Task<IActionResult> UpdatePaymentDetails(
        [FromRoute] Guid id,
        [FromBody] PaymentDetailsListDto paymentDetails,
        [FromServices] UpdateVolunteerPaymentDetailsHandler handler,
        [FromServices] IValidator<UpdateVolunteerPaymentDetailsRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateVolunteerPaymentDetailsRequest(id, paymentDetails);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var handleResult = await handler.Update(request, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }
}
