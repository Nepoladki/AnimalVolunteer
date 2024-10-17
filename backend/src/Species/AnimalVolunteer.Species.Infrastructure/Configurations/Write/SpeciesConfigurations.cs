using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainEntity = AnimalVolunteer.Species.Domain.Root;

namespace AnimalVolunteer.Species.Infrastructure.Configurations.Write;

public class SpeciesConfigurations : IEntityTypeConfiguration<DomainEntity.Species>
{
    public void Configure(EntityTypeBuilder<DomainEntity.Species> builder)
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
            .HasForeignKey(b => b.SpeciesId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}