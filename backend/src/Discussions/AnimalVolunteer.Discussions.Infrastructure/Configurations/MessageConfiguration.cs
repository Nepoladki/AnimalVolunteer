using AnimalVolunteer.Discussions.Domain.Aggregate.Entities;
using AnimalVolunteer.Discussions.Domain.Aggregate.ValueObjects;
using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Discussions.Infrastructure.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .ToTable("messages");

        builder
            .HasKey(m => m.Id);

        builder
            .Property(m => m.Id)
            .HasConversion(
                vo => vo.Value,
                db => MessageId.CreateWithGuid(db))
            .IsRequired()
            .HasColumnName("id");

        builder
            .Property(m => m.UserId)
            .IsRequired()
            .HasColumnName("user_id");

        builder
            .Property(m => m.Text)
            .HasConversion(
                vo => vo.Value,
                db => Text.Create(db).Value)
            .IsRequired()
            .HasColumnName("text");
            

        builder
            .Property(m => m.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");

        builder
            .Property(m => m.IsEdited)
            .IsRequired()
            .HasColumnName("is_edited");
            
    }
}

