using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Core.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AnimalVolunteer.Accounts.Infrastructure;
public class AuthDbContext : IdentityDbContext<User, Role, Guid>
{
    private readonly IConfiguration _config;

    public AuthDbContext(IConfiguration configuration)
    {
        _config = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_config.GetConnectionString(DatabaseOptions.SECTION_NAME));
        builder.UseSnakeCaseNamingConvention();
        builder.EnableSensitiveDataLogging();
        builder.UseLoggerFactory(new LoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("users");
        builder.Entity<Role>().ToTable("roles");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");
    }
}  
