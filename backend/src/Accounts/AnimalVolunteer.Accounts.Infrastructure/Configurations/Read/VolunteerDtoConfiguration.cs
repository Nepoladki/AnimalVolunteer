using AnimalVolunteer.Core.DTOs.Accounts;
using AnimalVolunteer.Core.DTOs.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace AnimalVolunteer.Accounts.Infrastructure.Configurations.Read;

public class VolunteerDtoConfiguration : IEntityTypeConfiguration<VolunteerAccountDto>
{
    public void Configure(EntityTypeBuilder<VolunteerAccountDto> builder)
    {
        builder.ToTable("volunteer_accounts");

        builder.HasKey(va => va.Id);

        builder.Property(va => va.PaymentDetails)
            .HasConversion(
            pd => JsonSerializer.Serialize(pd, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<List<PaymentDetailsDto>>(json, JsonSerializerOptions.Default)!);

        builder.Property(va => va.Certificates)
            .HasConversion(
            c => JsonSerializer.Serialize(c, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<List<CertificateDto>>(json, JsonSerializerOptions.Default)!);
    }
}

