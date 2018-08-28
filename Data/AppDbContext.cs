using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParityUI.Models;

namespace ParityUI.Data
{
  public class AppDbContext : IdentityDbContext<AppUser>
  {
    public DbSet<AccountLink> LinkedAccounts { get; set; }

    public DbSet<Credentials> Credentials { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<AccountLink>().HasKey(link => new { link.Id, link.AppUserId });
      builder.Entity<AccountLink>().Property(link => link.Id).ValueGeneratedOnAdd();

      builder.Entity<Credentials>().HasKey(credentials => new { credentials.AccountLinkId, credentials.AppUserId });

      builder.Entity<AccountLink>()
        .HasOne(link => link.Credentials)
        .WithOne(creds => creds.AccountLink)
        .HasForeignKey<Credentials>(credentials => new { credentials.AccountLinkId, credentials.AppUserId });
    }
  }
}
