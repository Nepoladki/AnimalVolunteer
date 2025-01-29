using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AnimalVolunteer.Core.DTOs.Volunteers.Pet;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Pet;
using AnimalVolunteer.Volunteers.Domain.Enums;
using AnimalVolunteer.Core.DTOs.Common;

namespace AnimalVolunteer.Volunteers.Infrastructure.Configurations.Read;

public class PetDtoConfigurations : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PaymentDetails)
            .HasConversion(
            values => JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default),
            json => JsonSerializer
               .Deserialize<IEnumerable<PaymentDetailsDto>>(
                   json, JsonSerializerOptions.Default)!)
            .HasColumnName(PaymentDetails.DB_COLUMN_NAME);

        builder.Property(x => x.Photos)
           .HasConversion(
           values => JsonSerializer
               .Serialize(string.Empty, JsonSerializerOptions.Default),
           json => JsonSerializer
               .Deserialize<IEnumerable<PetPhotoDto>>(
                   json, JsonSerializerOptions.Default)!)
           .HasColumnName(PetPhoto.DB_COLUMN_NAME);

        builder.Property(p => p.CurrentStatus)
            .HasConversion(
            into => into.ToString(),
            dbValue => (CurrentStatus)Enum.Parse(typeof(CurrentStatus), dbValue))
            .IsRequired()
            .HasColumnName("current_status");
    }
}
