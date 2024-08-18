using AnimalVolunteer.Domain.Aggregates;
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

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.CreateWithGuid(value))
            .HasColumnName("id");

        builder.ComplexProperty(s => s.Title, t =>
        {
            t.Property(i => i.Value)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
            .HasColumnName("title");
        });

        //builder.ComplexProperty(b => b.SpeciesId, i =>
        //{
        //    i.Property(i => i.Value)
        //    .IsRequired()
        //    .HasColumnName("species_id");
        //});

        builder.HasOne<Species>()
            .WithMany(s => s.Breeds)
            .HasForeignKey(s => s.SpeciesId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
