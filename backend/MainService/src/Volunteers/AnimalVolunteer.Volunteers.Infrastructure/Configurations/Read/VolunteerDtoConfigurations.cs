using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using AnimalVolunteer.Domain.Common.ValueObjects;
using AnimalVolunteer.Core.DTOs.Volunteers;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
using AnimalVolunteer.Core.DTOs.Common;

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
