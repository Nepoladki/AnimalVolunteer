using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Aggregates;

namespace AnimalVolunteer.Infrastructure.Configurations;

public class VolunteerConfigurations : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(x => x.Id)
            .HasName("volunteer_id");

        builder.Property(x => x.Id)
            .HasConversion(
            id => id.Value,
            value => VolunteerId.CreateWithGuid(value));

        builder.ComplexProperty(x => x.FullName, fn =>
        {
            fn.Property(i => i.FirstName)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
            .HasColumnName("first_name");

            fn.Property(i => i.SurName)
            .IsRequired(false)
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
            .HasColumnName("surname");

            fn.Property(i => i.LastName)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
            .HasColumnName("last_name");
        });

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

        builder.OwnsOne(x => x.ContactInfos, ci => 
        {
            ci.ToJson();

            ci.OwnsMany(i => i.Contacts, j =>
            {
                j.Property(k => k.PhoneNumber)
                .IsRequired()
                .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
                .HasJsonPropertyName("phone_number");

                j.Property(k => k.Name)
                .IsRequired()
                .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
                .HasJsonPropertyName("name");

                j.Property(k => k.Note)
                .IsRequired(false)
                .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_MEDIUM)
                .HasJsonPropertyName("note");
            });
        });

        builder.OwnsOne(x => x.SocialNetworks, ci =>
        {
            ci.ToJson();

            ci.OwnsMany(i => i.SocialNetworks, j =>
            {
                j.Property(k => k.Name)
                .IsRequired()
                .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
                .HasJsonPropertyName("name");

                j.Property(k => k.URL)
                .IsRequired()
                .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_MEDIUM)
                .HasJsonPropertyName("url");
            });
        });

        builder.OwnsOne(x => x.PaymentDetails, pd =>
        {
            pd.ToJson();

            pd.OwnsMany(i => i.Payments, j =>
            {
                j.Property(k => k.Name)
                .IsRequired()
                .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
                .HasJsonPropertyName("name");

                j.Property(k => k.Description)
                .IsRequired()
                .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
                .HasJsonPropertyName("description");
            });
        });

        builder.HasMany(x => x.Pets)
            .WithOne()
            .HasForeignKey("pet_id");
    }
}
