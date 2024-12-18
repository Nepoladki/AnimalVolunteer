﻿using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Species.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Species.Infrastructure.Configurations.Write;

public class BreedConfigurations : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => BreedId.CreateWithGuid(value));

        builder.ComplexProperty(s => s.Name, t =>
        {
            t.Property(i => i.Value)
            .IsRequired()
            .HasMaxLength(Name.MAX_NAME_LENGTH)
            .HasColumnName(Name.DB_COLUMN_NAME);
        });
    }
}
