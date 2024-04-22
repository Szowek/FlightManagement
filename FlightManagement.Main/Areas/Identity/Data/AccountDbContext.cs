using FlightManagement.Main.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightManagement.Main.Areas.Identity.Data;

public class AccountDbContext : IdentityDbContext<AccountMainUser>
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserEntityConfiguration());
    }
}

public class UserEntityConfiguration : IEntityTypeConfiguration<AccountMainUser>
{
    public void Configure(EntityTypeBuilder<AccountMainUser> builder)
    {
        builder.Property(u => u.UserName).HasMaxLength(20);
    }
}