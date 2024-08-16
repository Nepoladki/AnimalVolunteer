using AnimalVolunteer.Domain.Aggregates;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.ValueObjects.Common;
using AnimalVolunteer.Domain.ValueObjects.Species;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Infrastructure.Configurations;

public class SpeciesConfigurations : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id)
            .HasName("species_id");

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                guid => SpeciesId.CreateWithGuid(guid));

        builder.Property(s => s.Title)
            .HasConversion(
                t => t.Value, 
                value => Title.Create(value).Value)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("breed_id");
    }
}
