using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Infrastructure.Configurations;

public class PaymentDetailsConfiguration : IEntityTypeConfiguration<PaymentDetails>
{
    public void Configure(EntityTypeBuilder<PaymentDetails> builder)
    {
        builder.ToTable("payment_details");

        builder.HasKey(x => x.PaymentDetalisId);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_MEDIUM);
    }
}
