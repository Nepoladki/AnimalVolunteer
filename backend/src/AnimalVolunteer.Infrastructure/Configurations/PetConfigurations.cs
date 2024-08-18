using AnimalVolunteer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.ValueObjects.Pet;

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

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_HIGH);

        builder.Property(x => x.Color)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.ComplexProperty(x => x.SpeciesAndBreed, sb =>
        {
            sb.Property(j => j.SpeciesId)
            .IsRequired()
            .HasColumnName("species_id");

            sb.Property(j => j.BreedId)
            .IsRequired()
            .HasColumnName("breed_id");
        });

        builder.ComplexProperty(x => x.HealthInfo, hi =>
        {
            hi.Property(j => j.Description)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_HIGH)
            .HasColumnName("description");

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
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
            .HasColumnName("country");

            a.Property(j => j.City)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
            .HasColumnName("city");

            a.Property(j => j.Street)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
            .HasColumnName("street");

            a.Property(j => j.House)
            .IsRequired(false)
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW)
            .HasColumnName("house");
        });

        builder.Property(x => x.Weight)
            .IsRequired();

        builder.Property(x => x.Height)
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
                .HasJsonPropertyName("note");
            });
        });

        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.CurrentStatus)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

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

        builder.OwnsOne(x => x.PetPhotos, pp =>
        {
            pp.ToJson();

            pp.OwnsMany(i => i.PetPhotos, j =>
            {
                j.Property(k => k.FilePath)
                .IsRequired()
                .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_MEDIUM)
                .HasJsonPropertyName("file_path");

                j.Property(k => k.IsMain)
                .IsRequired()
                .HasJsonPropertyName("is_main");
            });
        });
    }
}
