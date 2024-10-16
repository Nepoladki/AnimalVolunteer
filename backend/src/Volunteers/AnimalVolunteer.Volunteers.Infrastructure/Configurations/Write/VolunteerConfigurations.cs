using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnimalVolunteer.Domain.Common.ValueObjects;
using AnimalVolunteer.Domain.Aggregates.VolunteerManagement.ValueObjects.Volunteer;
using AnimalVolunteer.Volunteers.Domain.Root;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.Volunteers.Domain.ValueObjects.Volunteer;
using AnimalVolunteer.SharedKernel.ValueObjects;
using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Core.DTOs.Volunteers;

namespace AnimalVolunteer.Volunteers.Infrastructure.Configurations.Write;

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
            .HasColumnName(FullName.DB_COLUMN_FIRSTNAME);

            fn.Property(i => i.SurName)
            .IsRequired(false)
            .HasMaxLength(FullName.MAX_NAME_LENGTH)
            .HasColumnName(FullName.DB_COLUMN_SURNAME);

            fn.Property(i => i.LastName)
            .IsRequired()
            .HasMaxLength(FullName.MAX_NAME_LENGTH)
            .HasColumnName(FullName.DB_COLUMN_LASTNAME);
        });

        builder.ComplexProperty(x => x.Email, eb =>
        {
            eb.Property(i => i.Value)
            .IsRequired()
            .HasMaxLength(Email.MAX_LENGTH)
            .HasColumnName(Email.DB_COLUMN_NAME);
        });

        builder.ComplexProperty(x => x.Description, db =>
        {
            db.Property(i => i.Value)
            .IsRequired()
            .HasMaxLength(Description.MAX_DESC_LENGTH)
            .HasColumnName(Description.DB_COLUMN_NAME);
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

        builder.Property(x => x.ContactInfoList)
          .HasValueObjectsJsonConversion(
          dm => new ContactInfoDto(dm.PhoneNumber, dm.Name, dm.Note),
          dto => ContactInfo
                  .Create(dto.PhoneNumber, dto.Name, dto.Note).Value)
          .HasColumnName(ContactInfo.DB_COLUMN_NAME);

        builder.Property(x => x.SocialNetworksList)
             .HasValueObjectsJsonConversion(
            dm => new SocialNetworkDto(dm.Name, dm.URL),
            dto => SocialNetwork.Create(dto.Name, dto.URL).Value)
             .HasColumnName(SocialNetwork.DB_COLUMN_NAME);

        builder.Property(x => x.PaymentDetailsList)
            .HasValueObjectsJsonConversion(
            dm => new PaymentDetailsDto(dm.Name, dm.Description),
            dto => PaymentDetails.Create(dto.Name, dto.Description).Value)
            .HasColumnName(PaymentDetails.DB_COLUMN_NAME);

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey(p => p.VolunteerId)
            .IsRequired();
    }
}
