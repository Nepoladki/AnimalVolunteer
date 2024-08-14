using AnimalVolunteer.Domain.Common;
using AnimalVolunteer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Infrastructure.Configurations;

public class SocialNetworkConfiguration : IEntityTypeConfiguration<SocialNetwork>
{
    public void Configure(EntityTypeBuilder<SocialNetwork> builder)
    {
        builder.ToTable("social_network");

        builder.HasKey(x => x.SocialNetworkId);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_LOW);

        builder.Property(x => x.URL)
            .IsRequired()
            .HasMaxLength(Constants.TEXT_LENGTH_LIMIT_MEDIUM);
    }
}
