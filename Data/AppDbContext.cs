using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParityUI.Models;

namespace ParityUI.Data
{
  public class AppDbContext : IdentityDbContext<AppUser>
  {
    public DbSet<LinkedAccount> LinkedAccounts { get; set; }

    public DbSet<Credentials> Credentials { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<LinkedAccount>().HasKey(link => new { link.Id, link.AppUserId });
      builder.Entity<LinkedAccount>().Property(link => link.Id).ValueGeneratedOnAdd();

      builder.Entity<Credentials>().HasKey(credentials => new { credentials.LinkedAccountId, credentials.AppUserId });

      builder.Entity<LinkedAccount>()
        .HasOne(link => link.Credentials)
        .WithOne(creds => creds.LinkedAccount)
        .HasForeignKey<Credentials>(credentials => new { credentials.LinkedAccountId, credentials.AppUserId });
    }
  }
}
