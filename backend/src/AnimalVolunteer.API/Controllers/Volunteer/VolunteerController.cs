using AnimalVolunteer.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using AnimalVolunteer.API.Processors;
using AnimalVolunteer.API.Controllers.Volunteer.Requests.Pet;
using AnimalVolunteer.API.Controllers.Volunteer.Requests.Volunteer;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.AddPet;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Delete;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Create;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Update.ContactInfo;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Update.MainInfo;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Update.PaymentDetails;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Volunteer.Update.SocialNetworks;
using AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Volunteer.GetVolunteerById;
using AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Volunteer.GetVolunteersWithPagination;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.UpdatePetPhotos;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.DeletePetPhotos;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.UpdatePet;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.ChangePetStatus;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.SoftDeletePet;
using AnimalVolunteer.Application.Features.VolunteerManagement.Commands.Pet.HardDeletePet;
using AnimalVolunteer.Application.Features.VolunteerManagement.Queries.Pet.GetPetsFilteredPaginated;

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

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromServices] GetVolunteerByIdQueryHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetVolunteerByIdQuery(id);

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response.Value);
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

        return Ok(creationResult.Value);
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
        [FromForm] CreatePetRequest request,
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
        [FromForm] UpdatePetPhotosRequest request,
        [FromServices] UpdatePetPhotosHandler handler,
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

    [HttpDelete("{volunteerId:guid}/pets/{petId:guid}/photos")]
    public async Task<IActionResult> DeletePetPhotos(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeletePetPhotosHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeletePetPhotosCommand(volunteerId, petId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [HttpPut("{volunteerId:guid}/pets/{petId:guid}")]
    public async Task<IActionResult> UpdatePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] UpdatePetHandler handler,
        [FromBody] UpdatePetRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(volunteerId, petId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [HttpPut("{volunteerId:guid}/pets/{petId:guid}/status")]
    public async Task<IActionResult> ChangePetStatus(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] ChangePetStatusRequest request,
        [FromServices] ChangePetStatusHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(volunteerId, petId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [HttpDelete("{volunteerId:guid}/pets/{petId:guid}/soft")]
    public async Task<IActionResult> SoftDeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] SoftDeletePetHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new SoftDeletePetCommand(volunteerId, petId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [HttpDelete("{volunteerId:guid}/pets/{petId:guid}/hard")]
    public async Task<IActionResult> HardDeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] HardDeletePetHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new HardDeletePetCommand(volunteerId, petId);

        var handleResult = await handler.Handle(command, cancellationToken);
        if (handleResult.IsFailure)
            return handleResult.Error.ToResponse();

        return Ok();
    }

    [HttpGet("pets")]
    public async Task<IActionResult> GetPets(
        [FromQuery] GetPetsFilteredPaginatedRequest request,
        [FromServices] GetPetsFilteredPaginatedHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var pets = await handler.Handle(query, cancellationToken);

        return Ok(pets);
    }
}
