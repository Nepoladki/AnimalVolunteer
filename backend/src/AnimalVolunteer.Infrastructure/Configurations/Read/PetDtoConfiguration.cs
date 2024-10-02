using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AnimalVolunteer.Application.DTOs.Volunteer;

namespace AnimalVolunteer.Infrastructure.Configurations.Read;

public class PetDtoConfigurations : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);
    }
}
