using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Entities;
using AnimalVolunteer.Domain.ValueObjects.Breed;
using AnimalVolunteer.Domain.ValueObjects.Common;
using AnimalVolunteer.Domain.ValueObjects.Species;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Infrastructure.Configurations;

public class BreedConfigurations : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(x => x.Id)
            .HasName("breed_id");

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.CreateWithGuid(value));

        builder.Property(s => s.Title)
            .HasConversion(
                t => t.Value,
                value => Title.Create(value).Value)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.Property(x => x.SpeciesId)
            .HasConversion(
                id => id.Value,
                value => SpeciesId.CreateWithGuid(value))
            .IsRequired();
    }
}
