using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AnimalVolunteer.Application.DTOs.Volunteer;

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
    }
}
