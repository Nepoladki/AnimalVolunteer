using AnimalVolunteer.Domain.Aggregates.PetType;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Infrastructure.Configurations;

public class SpeciesConfigurations : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                guid => SpeciesId.CreateWithGuid(guid))
            .HasColumnName("id");

        builder.ComplexProperty(s => s.Title, t =>
        {
            t.Property(i => i.Value)
            .IsRequired()
            .HasMaxLength(Title.MAX_LENGTH)
            .HasColumnName("title");
        });

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}
