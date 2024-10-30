﻿using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;
using System.Text.Json;

namespace AnimalVolunteer.Accounts.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.HasOne(u => u.Role);

        builder.ComplexProperty(u => u.FullName, builder =>
        {
            builder.Property(fn => fn.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(FullName.MAX_LENGTH)
                .IsRequired();

            builder.Property(fn => fn.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(FullName.MAX_LENGTH)
                .IsRequired();

            builder.Property(fn => fn.Patronymic)
                .HasColumnName("patronymic")
                .HasMaxLength(FullName.MAX_LENGTH);
        });

        builder.Property(u => u.SocialNetworks)
            .HasConversion(
            sn => JsonSerializer.Serialize(sn, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<List<SocialNetwork>>(json, JsonSerializerOptions.Default)!);
    }
}
