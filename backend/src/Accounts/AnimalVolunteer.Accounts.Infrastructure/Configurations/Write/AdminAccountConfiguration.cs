using AnimalVolunteer.Accounts.Domain.Models.AccountTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AnimalVolunteer.Accounts.Infrastructure.Configurations;

public class AdminAccountConfiguration : IEntityTypeConfiguration<AdminAccount>
{
    public void Configure(EntityTypeBuilder<AdminAccount> builder)
    {
        builder.ToTable("admin_accounts");

        builder.HasKey(aa => aa.Id);
    }
}
