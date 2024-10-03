using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AnimalVolunteer.Application.DTOs.Volunteer;
using System.Text.Json;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;

namespace AnimalVolunteer.Infrastructure.Configurations.Read;

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
                   json, JsonSerializerOptions.Default)!);

        builder.Property(x => x.Photos)
           .HasConversion(
           values => JsonSerializer
               .Serialize(string.Empty, JsonSerializerOptions.Default),
           json => JsonSerializer
               .Deserialize<IEnumerable<PetPhotoDto>>(
                   json, JsonSerializerOptions.Default)!);
    }
}
