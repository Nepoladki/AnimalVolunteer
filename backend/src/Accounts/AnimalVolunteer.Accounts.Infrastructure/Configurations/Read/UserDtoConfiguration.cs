using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using AnimalVolunteer.Core.DTOs.Accounts;
using AnimalVolunteer.Core.DTOs.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace AnimalVolunteer.Accounts.Infrastructure.Configurations.Read;

public class UserDtoConfiguration : IEntityTypeConfiguration<UserDto>
{
    public void Configure(EntityTypeBuilder<UserDto> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.VolunteerAccount)
            .WithOne()
            .HasForeignKey<VolunteerAccountDto>(va => va.UserId);
        
        builder.HasOne(x => x.ParticipantAccount)
            .WithOne()
            .HasForeignKey<ParticipantAccountDto>(pa => pa.UserId);
        
        builder.HasOne(x => x.AdminAccount)
            .WithOne()
            .HasForeignKey<AdminAccountDto>(aa => aa.UserId);

        builder.Property(u => u.SocialNetworks)
            .HasConversion(
            sn => JsonSerializer.Serialize(sn, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<List<SocialNetworkDto>>(json, JsonSerializerOptions.Default)!)
            .HasColumnName(SocialNetwork.DB_COLUMN_NAME);
    }
}

