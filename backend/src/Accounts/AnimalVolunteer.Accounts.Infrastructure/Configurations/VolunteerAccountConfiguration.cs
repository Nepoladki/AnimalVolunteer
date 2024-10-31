using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using AnimalVolunteer.Accounts.Domain.Models.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace AnimalVolunteer.Accounts.Infrastructure.Configurations;

public class VolunteerAccountConfiguration : IEntityTypeConfiguration<VolunteerAccount>
{
    public void Configure(EntityTypeBuilder<VolunteerAccount> builder)
    {
        builder.ToTable("volunteer_accounts");

        builder.HasKey(va => va.Id);

        builder.Property(va => va.PaymentDetails)
            .HasConversion(
            pd => JsonSerializer.Serialize(pd, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<List<PaymentDetails>>(json, JsonSerializerOptions.Default)!);
        
        builder.Property(va => va.Certificates)
            .HasConversion(
            c => JsonSerializer.Serialize(c, JsonSerializerOptions.Default),
            json => JsonSerializer.Deserialize<List<Certificate>>(json, JsonSerializerOptions.Default)!);
    }
}
