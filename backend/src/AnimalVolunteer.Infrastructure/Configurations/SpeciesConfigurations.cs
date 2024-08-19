﻿using AnimalVolunteer.Domain.Aggregates;
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
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
            .HasColumnName("title");
        });

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}
