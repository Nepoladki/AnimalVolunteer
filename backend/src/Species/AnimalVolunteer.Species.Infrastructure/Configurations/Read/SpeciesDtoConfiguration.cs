﻿using AnimalVolunteer.Core.DTOs.Species;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Species.Infrastructure.Configurations.Read;

public class SpeciesDtoConfiguration : IEntityTypeConfiguration<SpeciesDto>
{
    public void Configure(EntityTypeBuilder<SpeciesDto> builder)
    {
        builder.ToTable("species");

        builder.HasKey(s => s.Id);

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey(b => b.SpeciesId);
    }
}
