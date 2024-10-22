using AnimalVolunteer.Accounts.Domain.Models;
using AnimalVolunteer.Core.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AnimalVolunteer.Accounts.Infrastructure;
public class AuthDbContext : IdentityDbContext<User, Role, Guid>
{
    private readonly IConfiguration _config;
    private readonly DatabaseOptions _dbOptions;

    public AuthDbContext(
        IConfiguration configuration, 
        IOptions<DatabaseOptions> dbOptions)
    {
        _dbOptions = dbOptions.Value;
        _config = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseNpgsql(_config.GetConnectionString(_dbOptions.PostgresConnectionName));
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

        builder.Entity<RolePermission>().ToTable("roles_permissions")
            .HasKey(rp => new {rp.RoleId, rp.PermissionId});

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(rp => rp.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId);
    }
}  
