using AnimalVolunteer.Application.DTOs.VolunteerManagement.Pet;
using AnimalVolunteer.Application.Models;
using AnimalVolunteer.Domain.Common;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AnimalVolunteer.Volunteers.Application.Queries.Pet.GetPetById;

public class GetPetByIdHandler : IQueryHandler<Result<PetDto, Error>, GetPetByIdQuery>
{
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<GetPetByIdQuery> _validator;

    public GetPetByIdHandler(
        IReadDbContext readDbContext,
        IValidator<GetPetByIdQuery> validator)
    {
        _readDbContext = readDbContext;
        _validator = validator;
    }

    public async Task<Result<PetDto, Error>> Handle(
        GetPetByIdQuery query, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);

        var pet = await _readDbContext.Pets
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);
        if (pet is null)
            return Errors.General.NotFound(query.Id);

        return pet;
    }
}
