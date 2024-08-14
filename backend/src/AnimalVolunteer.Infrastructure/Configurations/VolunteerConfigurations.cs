using AnimalVolunteer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnimalVolunteer.Domain.Common;

namespace AnimalVolunteer.Infrastructure.Configurations;

public class VolunteerConfigurations : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(x => x.VolunteerId);

        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_HIGH);

        builder.Property(x => x.ExpirienceYears)
            .IsRequired();

        builder.Property(x => x.PetsFoundedHome)
            .IsRequired();

        builder.Property(x => x.PetsLookingForHome)
            .IsRequired();

        builder.Property(x => x.PetsInVetClinic)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .IsRequired();

        builder.HasMany(x => x.SocialNetworks)
            .WithOne()
            .HasForeignKey("social_network_id");

        builder.HasMany(x => x.PaymentDetails)
            .WithOne()
            .HasForeignKey("payment_details_id");

        builder.HasMany(x => x.Pets)
            .WithOne()
            .HasForeignKey("pet_id");
    }
}
