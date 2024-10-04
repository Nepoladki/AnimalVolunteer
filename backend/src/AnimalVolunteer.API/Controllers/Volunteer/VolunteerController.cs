using AnimalVolunteer.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using AnimalVolunteer.API.Processors;
using AnimalVolunteer.API.Controllers.Volunteer.Requests.Pet;
using AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.AddPet;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.AddPetPhotos;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Create;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Delete;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.ContactInfo;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.MainInfo;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.PaymentDetails;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Update.SocialNetworks;
using AnimalVolunteer.Application.Features.VolunteerManagement.Queries.GetVolunteersWithPagination;
using AnimalVolunteer.API.Response;

namespace AnimalVolunteer.API.Controllers.Volunteer;
public class VolunteerController : ApplicationController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] GetFilteredVolunteersWithPaginationRequest request,
        [FromServices] GetFilteredVolunteersWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateVolunteerRequest request,
        [FromServices] CreateVolunteerHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var creationResult = await handler.Handle(command, cancellationToken);

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
        
        var command = request.ToCommand(id);

        var handleResult = await handler.Handle(command, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<IActionResult> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromBody] UpdateVolunteerSocialNetworksRequest request,
        [FromServices] UpdateVolunteerSocialNetworksHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var handleResult = await handler.Handle(command, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpPut("{id:guid}/payment-details")]
    public async Task<IActionResult> UpdatePaymentDetails(
        [FromRoute] Guid id,
        [FromBody] UpdateVolunteerPaymentDetailsRequest request,
        [FromServices] UpdateVolunteerPaymentDetailsHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var handleResult = await handler.Handle(command, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok(handleResult.Value);
    }

    [HttpPut("{id:guid}/contact-info")]
    public async Task<IActionResult> UpdateContactInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateVolunteerContactInfoRequest request,
        [FromServices] UpdateVolunteerContactInfoHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

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
            .Handle(new DeleteVolunteerCommand(id), cancellationToken);
        if (deleteResult.IsFailure)
            return deleteResult.Error.ToResponse();

        return Ok(deleteResult.Value);
    }

    [HttpPost("{id:guid}/pets")]
    public async Task<IActionResult> AddPet(
        [FromRoute] Guid id,
        [FromForm] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var addResult = await handler.Handle(command, cancellationToken);
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

        var command = request.ToCommand(
            volunteerId, petId, fileList);

        var handleResult = await handler
            .Handle(command, cancellationToken);

        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }
}
