using AnimalVolunteer.Accounts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Accounts.Infrastructure.Configurations;

public class RefreshSessionConfiguration : IEntityTypeConfiguration<RefreshSession>
{
    public void Configure(EntityTypeBuilder<RefreshSession> builder)
    {
        builder.ToTable(RefreshSession.TABLE_NAME);

        builder.HasKey(t => t.Id);

        builder.HasOne(rs => rs.User)
            .WithMany()
            .HasForeignKey(rs => rs.UserId);
    }
}

