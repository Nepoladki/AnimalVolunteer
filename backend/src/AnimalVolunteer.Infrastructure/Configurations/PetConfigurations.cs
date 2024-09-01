using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Common.ValueObjects;

namespace AnimalVolunteer.Infrastructure.Configurations;

public class PetConfigurations : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => PetId.CreateWithGuid(value));

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");

        builder.ComplexProperty(x => x.Name, nd =>
        {
            nd.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(Name.MAX_NAME_LENGTH)
            .HasColumnName("name");
        });

        builder.ComplexProperty(x => x.Description, db =>
        {
            db.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(Description.MAX_DESC_LENGTH)
            .HasColumnName("description");
        });

        builder.ComplexProperty(x => x.SpeciesAndBreed, sb =>
        {
            sb.Property(j => j.SpeciesId)
            .IsRequired()
            .HasColumnName("species_id");

            sb.Property(j => j.BreedId)
            .IsRequired()
            .HasColumnName("breed_id");
        });

        builder.ComplexProperty(x => x.PhysicalParameters, sb =>
        {
            sb.Property(pp => pp.Color)
            .IsRequired()
            .HasMaxLength(PhysicalParameters.MAX_COLOR_LENGTH)
            .HasColumnName("color");

            sb.Property(pp => pp.Weight)
            .IsRequired()
            .HasColumnName("weight");

            sb.Property(pp => pp.Height)
            .IsRequired()
            .HasColumnName("height");
        });

        builder.ComplexProperty(x => x.HealthInfo, hi =>
        {
            hi.Property(j => j.Description)
            .IsRequired()
            .HasMaxLength(HealthInfo.MAX_DESC_LENGTH)
            .HasColumnName("health_description");

            hi.Property(j => j.IsVaccinated)
            .IsRequired()
            .HasColumnName("is_vaccinated");

            hi.Property(j => j.IsNeutered)
            .IsRequired()
            .HasColumnName("is_neutered");
        });

        builder.ComplexProperty(x => x.Address, a =>
        {
            a.Property(j => j.Country)
            .IsRequired()
            .HasMaxLength(Address.MAX_LENGTH)
            .HasColumnName("country");

            a.Property(j => j.City)
            .IsRequired()
            .HasMaxLength(Address.MAX_LENGTH)
            .HasColumnName("city");

            a.Property(j => j.Street)
            .IsRequired()
            .HasMaxLength(Address.MAX_LENGTH)
            .HasColumnName("street");

            a.Property(j => j.House)
            .IsRequired(false)
            .HasMaxLength(Address.MAX_LENGTH)
            .HasColumnName("house");
        });

        builder.OwnsOne(x => x.ContactInfos, ci =>
        {
            ci.ToJson(); 

            ci.OwnsMany(i => i.Contacts, j => 
            { 
                j.Property(k => k.PhoneNumber)
                .IsRequired()
                .HasMaxLength(ContactInfo.MAX_PHONE_LENGTH)
                .HasJsonPropertyName("phone_number");

                j.Property(k => k.Name)
                .IsRequired()
                .HasMaxLength(ContactInfo.MAX_NAME_LENGTH)
                .HasJsonPropertyName("name");

                j.Property(k => k.Note)
                .IsRequired(false)
                .HasMaxLength(ContactInfo.MAX_NOTE_LENGTH)
                .HasJsonPropertyName("note");
            });
        });

        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.CurrentStatus)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (CurrentStatus)Enum.Parse(typeof(CurrentStatus), v));

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.OwnsOne(x => x.PaymentDetails, pd =>
        {
            pd.ToJson();

            pd.OwnsMany(i => i.Payments, j =>
            {
                j.Property(k => k.Name)
                .IsRequired()
                .HasMaxLength(PaymentDetails.MAX_NAME_LENGTH)
                .HasJsonPropertyName("name");

                j.Property(k => k.Description)
                .IsRequired()
                .HasMaxLength(PaymentDetails.MAX_DESC_LENGTH)
                .HasJsonPropertyName("description");
            });
        });

        builder.OwnsOne(x => x.PetPhotos, pp =>
        {
            pp.ToJson();

            pp.OwnsMany(i => i.PetPhotos, j =>
            {
                j.Property(k => k.FilePath)
                .IsRequired() 
                .HasMaxLength(PetPhoto.MAX_FILEPATH_LENGTH)
                .HasJsonPropertyName("path");

                j.Property(k => k.IsMain)
                .IsRequired()
                .HasJsonPropertyName("is_main");
            });
        });
    }
}
