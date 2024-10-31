using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects;
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
                .HasColumnName(FullName.DB_COLUMN_FIRSTNAME)
                .HasMaxLength(FullName.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(fn => fn.LastName)
                .HasColumnName(FullName.DB_COLUMN_LASTNAME)
                .HasMaxLength(FullName.MAX_NAME_LENGTH)
                .IsRequired();

            builder.Property(fn => fn.Patronymic)
                .HasColumnName(FullName.DB_COLUMN_PATRONYMIC)
                .HasMaxLength(FullName.MAX_NAME_LENGTH);
        });

        builder.Property(u => u.SocialNetworks)
            .HasConversion(
            sn => JsonSerializer.Serialize(sn, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<List<SocialNetwork>>(json, JsonSerializerOptions.Default)!)
            .HasColumnName(SocialNetwork.DB_COLUMN_NAME);
    }
}
