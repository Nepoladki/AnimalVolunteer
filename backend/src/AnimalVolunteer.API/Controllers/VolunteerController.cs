using AnimalVolunteer.API.Extensions;
using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.Volunteer.CreateVolunteer;
using AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;
using AnimalVolunteer.Application.Features.Volunteer.Update.SocialNetworks;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AnimalVolunteer.API.Controllers;
public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateVolunteerRequest request,
        [FromServices] CreateVolunteerHandler _handler,
        CancellationToken cancellationToken = default)
    {
        var creationResult = await _handler.Create(request, cancellationToken);

        if (creationResult.IsFailure)
            return creationResult.Error.ToResponse();

        return Ok((Guid)creationResult.Value);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        Guid id,
        MainInfoDto mainInfo,
        [FromServices] UpdateVounteerMainInfoHandler _handler,
        [FromServices] IValidator<UpdateVounteerMainInfoRequest> _validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateVounteerMainInfoRequest(id, mainInfo);

        var validatonResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validatonResult.IsValid)
            return validatonResult.ToValidationErrorResponse();

         var handleResult = await _handler.Update(request, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<IActionResult> UpdateSocialNetworks(
        Guid id,
        SocialNetworksListDto socialNetworks,
        [FromServices] UpdateVolunteerSocialNetworksHandler _handler,
        [FromServices] IValidator<UpdateVolunteerSocialNetworksRequest> _validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateVolunteerSocialNetworksRequest(id, socialNetworks);

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var handleResult = await _handler.Update(request, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }
}
