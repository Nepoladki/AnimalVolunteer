using AnimalVolunteer.Core.DTOs.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnimalVolunteer.Accounts.Infrastructure.Configurations.Read;

public class ParticipantAccountDtoConfiguration : IEntityTypeConfiguration<ParticipantAccountDto>
{
    public void Configure(EntityTypeBuilder<ParticipantAccountDto> builder)
    {
        builder.ToTable("participant_accounts");

        builder.HasKey(pa => pa.Id);
    }
}

