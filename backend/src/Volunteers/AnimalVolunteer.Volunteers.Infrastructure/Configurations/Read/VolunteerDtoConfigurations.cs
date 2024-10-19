using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AnimalVolunteer.Domain.Common.ValueObjects;
using AnimalVolunteer.Core.DTOs.Volunteers;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;

namespace AnimalVolunteer.Volunteers.Infrastructure.Configurations.Read;

public class VolunteerDtoConfigurations : IEntityTypeConfiguration<VolunteerDto>
{
    public void Configure(EntityTypeBuilder<VolunteerDto> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Pets)
            .WithOne()
            .HasForeignKey(x => x.VolunteerId);

        builder.Property(x => x.PaymentDetails)
            .HasConversion(
            values => JsonSerializer
                .Serialize(string.Empty, JsonSerializerOptions.Default),
            json => JsonSerializer
                .Deserialize<IEnumerable<PaymentDetailsDto>>(
                    json, JsonSerializerOptions.Default)!)
            .HasColumnName(PaymentDetails.DB_COLUMN_NAME); ;

        builder.Property(x => x.SocialNetworks)
            .HasConversion(
            values => JsonSerializer
                .Serialize(string.Empty, JsonSerializerOptions.Default),
            json => JsonSerializer
                .Deserialize<IEnumerable<SocialNetworkDto>>(
                    json, JsonSerializerOptions.Default)!)
            .HasColumnName(SocialNetwork.DB_COLUMN_NAME);

        builder.Property(x => x.ContactInfos)
            .HasConversion(
            values => JsonSerializer
                .Serialize(string.Empty, JsonSerializerOptions.Default),
            json => JsonSerializer
                .Deserialize<IEnumerable<ContactInfoDto>>(
                    json, JsonSerializerOptions.Default)!)
            .HasColumnName(ContactInfo.DB_COLUMN_NAME); ;
    }
}
