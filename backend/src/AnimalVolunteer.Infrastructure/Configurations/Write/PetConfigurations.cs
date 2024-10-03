using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Entities;
using AnimalVolunteer.Domain.Aggregates.Volunteer.ValueObjects.Pet;
using AnimalVolunteer.Domain.Aggregates.Volunteer.Enums;
using AnimalVolunteer.Domain.Common.ValueObjects;
using AnimalVolunteer.Infrastructure.Extensions;
using AnimalVolunteer.Application.DTOs.Volunteer;
using AnimalVolunteer.Application.DTOs.Volunteer.Pet;

namespace AnimalVolunteer.Infrastructure.Configurations.Write;

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

        builder.ComplexProperty(x => x.Position, db =>
        {
            db.Property(z => z.Value)
            .IsRequired()
            .HasColumnName("position");
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

        builder.Property(x => x.ContactInfoList)
            .HasValueObjectsJsonConversion(
            dm => new ContactInfoDto(dm.PhoneNumber, dm.Name, dm.Note),
            dto => ContactInfo
                    .Create(dto.PhoneNumber, dto.Name, dto.Note).Value)
            .HasColumnName(ContactInfo.DB_COLUMN_NAME);

        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.CurrentStatus)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (CurrentStatus)Enum.Parse(typeof(CurrentStatus), v));

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.PaymentDetailsList)
            .HasValueObjectsJsonConversion(
            dm => new PaymentDetailsDto(dm.Name, dm.Description),
            dto => PaymentDetails.Create(dto.Name, dto.Description).Value)
            .HasColumnName(PaymentDetails.DB_COLUMN_NAME);

        builder.Property(x => x.PetPhotosList)
            .HasValueObjectsJsonConversion(
            dm => new PetPhotoDto(dm.FilePath.Value, dm.IsMain),
            dto => PetPhoto
                    .Create(FilePath
                        .Create(dto.FilePath).Value, dto.IsMain).Value)
            .HasColumnName(PetPhoto.DB_COLUMN_NAME);
    }
}
