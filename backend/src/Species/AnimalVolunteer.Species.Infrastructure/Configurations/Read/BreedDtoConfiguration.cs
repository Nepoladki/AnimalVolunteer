using AnimalVolunteer.Core.DTOs.Species;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Species.Infrastructure.Configurations.Read;

public class BreedDtoConfiguration : IEntityTypeConfiguration<BreedDto>
{
    public void Configure(EntityTypeBuilder<BreedDto> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(x => x.Id);
    }
}
