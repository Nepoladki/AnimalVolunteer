using AnimalVolunteer.SharedKernel.ValueObjects.EntityIds;
using AnimalVolunteer.VolunteerRequests.Domain;
using AnimalVolunteer.VolunteerRequests.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AnimalVolunteer.VolunteerRequests.Infrastructure.Configurations;
public class VolunteerRequestConfiguration : IEntityTypeConfiguration<VolunteerRequest>
{
    public void Configure(EntityTypeBuilder<VolunteerRequest> builder)
    {
        builder.ToTable("volunteer_requests");

        builder.HasKey(v => v.Id);

        builder
            .Property(v => v.Id)
            .HasConversion(
                vo => vo.Value,
                db => VolunteerRequestId.CreateWithGuid(db))
            .IsRequired()
            .HasColumnName("id");

        builder
            .Property(v => v.UserId)
            .HasConversion(
                vo => vo.Value,
                guid => UserId.CreateWithGuid(guid))
            .IsRequired()
            .HasColumnName("user_id");

        builder
            .Property(v => v.AdminId)
            .HasConversion(
                vo => vo.Value,
                guid => AdminId.CreateWithGuid(guid))
            .IsRequired()
            .HasColumnName("admin_id");

        builder
            .Property(v => v.DiscussionId)
            .HasConversion(
                vo => vo.Value,
                guid => DiscussionId.CreateWithGuid(guid))
            .IsRequired()
            .HasColumnName("discussion_id");

        builder
            .Property(v => v.Status)
            .HasConversion(
                enm => enm.ToString(),
                str => (VolunteerRequestStatus)Enum.Parse(typeof(VolunteerRequestStatus), str))
            .IsRequired()
            .HasColumnName("status_enum");

        builder
            .Property(v => v.RejectionComment)
            .HasColumnName("rejection_comment")
            .IsRequired(false);

        builder
            .ComplexProperty(v => v.VolunteerInfo, b =>
            {
                b.Property(vi => vi.ExpirienceDescription)
                .IsRequired()
                .HasColumnName("expirience_description");

                b.Property(vi => vi.Passport)
                .IsRequired()
                .HasColumnName("passport");
            });

        builder
            .Property(v => v.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
    }
}
