using AnimalVolunteer.Domain.Aggregates.PetType;
using AnimalVolunteer.Domain.Aggregates.PetType.ValueObjects;
using AnimalVolunteer.Domain.Common.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Infrastructure.Configurations.Write;

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

        builder.ComplexProperty(s => s.Name, t =>
        {
            t.Property(i => i.Value)
            .IsRequired()
            .HasMaxLength(Name.MAX_NAME_LENGTH)
            .HasColumnName(Name.DB_COLUMN_NAME);
        });

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}
