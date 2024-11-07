using AnimalVolunteer.Core.DTOs.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalVolunteer.Accounts.Infrastructure.Configurations.Read;

public class AdminAccountDtoConfiguration : IEntityTypeConfiguration<AdminAccountDto>
{
    public void Configure(EntityTypeBuilder<AdminAccountDto> builder)
    {
        builder.ToTable("admin_accounts");

        builder.HasKey(aa => aa.Id);
    }
}

