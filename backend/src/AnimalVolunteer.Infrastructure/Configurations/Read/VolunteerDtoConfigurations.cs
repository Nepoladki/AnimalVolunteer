using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AnimalVolunteer.Application.DTOs.Volunteer;
using System.Text.Json;

namespace AnimalVolunteer.Infrastructure.Configurations.Read;

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
                    json, JsonSerializerOptions.Default)!);

        builder.Property(x => x.SocialNetworks)
            .HasConversion(
            values => JsonSerializer
                .Serialize(string.Empty, JsonSerializerOptions.Default),
            json => JsonSerializer
                .Deserialize<IEnumerable<SocialNetworkDto>>(
                    json, JsonSerializerOptions.Default)!);

        builder.Property(x => x.ContactInfos)
            .HasConversion(
            values => JsonSerializer
                .Serialize(string.Empty, JsonSerializerOptions.Default),
            json => JsonSerializer
                .Deserialize<IEnumerable<ContactInfoDto>>(
                    json, JsonSerializerOptions.Default)!);
    }
}
