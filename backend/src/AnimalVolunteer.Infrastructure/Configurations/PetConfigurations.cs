using AnimalVolunteer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AnimalVolunteer.Domain.Common;
using System.Security.Cryptography.X509Certificates;

namespace AnimalVolunteer.Infrastructure.Configurations;

public class PetConfigurations : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.PetId);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.Property(x => x.Species)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_HIGH);

        builder.Property(x => x.Breed)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.Property(x => x.Color)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.Property(x => x.HealthInfo)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_HIGH);

        builder.Property(x => x.Address)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_MEDIUM);

        builder.Property(x => x.Weight)
            .IsRequired();

        builder.Property(x => x.Height)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.Property(x => x.IsNeutered)
            .IsRequired();

        builder.Property(x => x.IsVaccinated)
            .IsRequired();

        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.CurrentStatus)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.PaymentDetails)
            .WithOne()
            .HasForeignKey("payment_details_id");

        builder.HasMany(x => x.PetPhotos)
            .WithOne()
            .HasForeignKey("pet_photo_id");
    }
}
