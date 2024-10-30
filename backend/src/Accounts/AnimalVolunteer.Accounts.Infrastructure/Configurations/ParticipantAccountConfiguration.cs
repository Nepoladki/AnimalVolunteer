using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Accounts.Infrastructure.Configurations;

public class ParticipantAccountConfiguration : IEntityTypeConfiguration<ParticipantAccount>
{
    public void Configure(EntityTypeBuilder<ParticipantAccount> builder)
    {
        builder.ToTable("participant_accounts");
        
        builder.HasKey(pa => pa.Id);
    }
}
