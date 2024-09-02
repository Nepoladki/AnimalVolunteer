using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Aggregates.Volunteer;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Volunteer;
using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Infrastructure.Configurations;

public class VolunteerConfigurations : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
            id => id.Value,
            value => VolunteerId.CreateWithGuid(value));

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");

        builder.ComplexProperty(x => x.FullName, fn =>
        {
            fn.Property(i => i.FirstName)
            .IsRequired()
            .HasMaxLength(FullName.MAX_NAME_LENGTH)
            .HasColumnName("first_name");

            fn.Property(i => i.SurName)
            .IsRequired(false)
            .HasMaxLength(FullName.MAX_NAME_LENGTH)
            .HasColumnName("surname");

            fn.Property(i => i.LastName)
            .IsRequired()
            .HasMaxLength(FullName.MAX_NAME_LENGTH)
            .HasColumnName("last_name");
        });

        builder.ComplexProperty(x => x.Email, eb =>
        {
            eb.Property(i => i.Value)
            .IsRequired()
            .HasMaxLength(Email.MAX_LENGTH)
            .HasColumnName("email");
        });

        builder.ComplexProperty(x => x.Description, db =>
        {
            db.Property(i => i.Value)
            .IsRequired()
            .HasMaxLength(Description.MAX_DESC_LENGTH)
            .HasColumnName("description");
        });

        builder.ComplexProperty(x => x.Statistics, sb =>
        {
            sb.Property(i => i.ExpirienceYears)
            .IsRequired()
            .HasColumnName("expirience_years");

            sb.Property(i => i.PetsFoundedHome)
            .IsRequired()
            .HasColumnName("pets_founded_home");

            sb.Property(i => i.PetsLookingForHome)
            .IsRequired()
            .HasColumnName("pets_looking_for_home");

            sb.Property(i => i.PetsInVetClinic)
            .IsRequired()
            .HasColumnName("pets_in_vet_clinic");
        });

        builder.OwnsOne(x => x.ContactInfos, ci => 
        {
            ci.ToJson();

            ci.OwnsMany(i => i.Contacts, j =>
            {
                j.Property(k => k.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(ContactInfo.MAX_PHONE_LENGTH)
                .HasJsonPropertyName("phone_number");

                j.Property(k => k.Name)
                .IsRequired(false)
                .HasMaxLength(ContactInfo.MAX_NAME_LENGTH)
                .HasJsonPropertyName("name");

                j.Property(k => k.Note)
                .IsRequired(false)
                .HasMaxLength(ContactInfo.MAX_NOTE_LENGTH)
                .HasJsonPropertyName("note");
            });
        });

        builder.OwnsOne(x => x.SocialNetworks, ci =>
        {
            ci.ToJson();

            ci.OwnsMany(i => i.SocialNetworks, j =>
            {
                j.Property(k => k.Name)
                .IsRequired(false)
                .HasMaxLength(SocialNetwork.MAX_NAME_LENGTH)
                .HasJsonPropertyName("name");

                j.Property(k => k.URL)
                .IsRequired(false)
                .HasMaxLength(SocialNetwork.MAX_URL_LENGTH)
                .HasJsonPropertyName("url");
            });
        });

        builder.OwnsOne(x => x.PaymentDetails, pd =>
        {
            pd.ToJson();

            pd.OwnsMany(i => i.Payments, j =>
            {
                j.Property(k => k.Name)
                .IsRequired(false)
                .HasMaxLength(PaymentDetails.MAX_NAME_LENGTH)
                .HasJsonPropertyName("name");

                j.Property(k => k.Description)
                .IsRequired(false)
                .HasMaxLength(PaymentDetails.MAX_DESC_LENGTH)
                .HasJsonPropertyName("description");
            });
        });

        builder.HasMany(x => x.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
    }
}
