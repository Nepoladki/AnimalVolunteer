using AnimalVolunteer.API.Extensions;
using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.Features.Volunteer.CreateVolunteer;
using AnimalVolunteer.Application.Features.Volunteer.Update.MainInfo;
using AnimalVolunteer.Application.Features.Volunteer.Update.SocialNetworks;
using AnimalVolunteer.Application.Features.Volunteer.Update.PaymentDetails;
using Microsoft.AspNetCore.Mvc;
using AnimalVolunteer.Application.Features.Volunteer.Delete;
using AnimalVolunteer.Application.Features.Volunteer.AddPet;
using AnimalVolunteer.API.Processors;
using AnimalVolunteer.Application.Features.Volunteer.AddPetPhotos;
using AnimalVolunteer.API.Controllers.Volunteer.Requests;
using AnimalVolunteer.API.Controllers.Volunteer.Requests.Pet;

namespace AnimalVolunteer.API.Controllers.Volunteer;
public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateVolunteerCommand request,
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
        [FromBody] UpdateMainInfoRequest request,
        [FromServices] UpdateVolunteerMainInfoHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateVolunteerMainInfoCommand(
            id, 
            request.FirstName, 
            request.SurName, 
            request.LastName, 
            request.Email, 
            request.Description);

        var handleResult = await handler.Update(command, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<IActionResult> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromBody] SocialNetworksListDto socialNetworks,
        [FromServices] UpdateVolunteerSocialNetworksHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateVolunteerSocialNetworksCommand(id, socialNetworks);

        var handleResult = await handler.Update(command, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpPut("{id:guid}/payment-details")]
    public async Task<IActionResult> UpdatePaymentDetails(
        [FromRoute] Guid id,
        [FromBody] PaymentDetailsListDto paymentDetails,
        [FromServices] UpdateVolunteerPaymentDetailsHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateVolunteerPaymentDetailsCommand(id, paymentDetails);

        var handleResult = await handler.Update(command, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpPut("{id:guid}/contact-info")]
    public async Task<IActionResult> UpdateContactInfo(
        [FromRoute] Guid id,
        [FromBody] ContactInfoListDto contactInfo,
        [FromServices] UpdateVolunteerContactInfoHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateVolunteerContactInfoCommand(id, contactInfo);

        var handleResult = await handler.Handle(command, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        CancellationToken cancellationToken = default)
    {
        var deleteResult = await handler
            .Delete(new DeleteVolunteerCommand(id), cancellationToken);
        if (deleteResult.IsFailure)
            return deleteResult.Error.ToResponse();

        return Ok(deleteResult.Value);
    }

    [HttpPost("{id:guid}/pets")]
    public async Task<IActionResult> AddPet(
        [FromRoute] Guid id,
        [FromForm] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new AddPetCommand(
                id,
                request.Name,
                request.Description,
                request.Color,
                request.Weight,
                request.Height,
                request.SpeciesId,
                request.BreedId,
                request.HealthDescription,
                request.IsVaccinated,
                request.IsNeutered,
                request.Country,
                request.City,
                request.Street,
                request.House,
                request.BirthDate,
                request.CurrentStatus);

        var addResult = await handler.Add(command, cancellationToken);
        if (addResult.IsFailure)
            return addResult.Error.ToResponse();

        return Ok(addResult.Value);
    }

    [HttpPost("{volunteerId:guid}/pets/{petId:guid}/photos")]
    public async Task<IActionResult> AddPetPhotos(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm] AddPetPhotosRequest request,
        [FromServices] AddPetPhotosHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var fileProcessor = new FormFileProcessor();

        var fileList = fileProcessor.Process(request.Files);

        var command = new AddPetPhotosCommand(
            volunteerId, petId, fileList);

        var handleResult = await handler
            .Handle(command, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }
}
