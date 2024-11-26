using AnimalVolunteer.Core.Extensions;
using AnimalVolunteer.Discussions.Domain.Aggregate;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace AnimalVolunteer.Discussions.Infrastructure.Configurations;

public class DiscussionConfiguration : IEntityTypeConfiguration<Discussion>
{
    public void Configure(EntityTypeBuilder<Discussion> builder)
    {
        builder
            .ToTable("discussions");

        builder
            .HasKey(d => d.Id);

        builder
            .Property(d => d.Id)
            .HasConversion(
                vo => vo.Value,
                db => DiscussionId.CreateWithGuid(db))
            .IsRequired()
            .HasColumnName("id");

        builder
            .HasMany(d => d.Messages)
            .WithOne(m => m.Discussion)
            .HasForeignKey("discussion_id");

        builder
            .Property(d => d.RelationId)
            .IsRequired()
            .HasColumnName("related_entity");

        builder
            .Property(d => d.UsersIds)
            .HasField("_usersIds")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired()
            .HasColumnName("users_ids");

        builder
            .Property(d => d.IsOpened)
            .IsRequired()
            .HasColumnName("is_opened");
    }
}

