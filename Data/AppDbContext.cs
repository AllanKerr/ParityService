using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParityService.Models.Entities;

namespace ParityService.Data
{
  public class AppDbContext : IdentityDbContext<User>
  {
    public DbSet<ServiceLink> ServiceLinks { get; set; }
    public DbSet<Earnings> Earnings { get; set; }
    public DbSet<Credentials> Credentials { get; set; }
    public DbSet<Account> Accounts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<ServiceLink>().HasKey(link => new { link.Id, link.UserId });
      builder.Entity<ServiceLink>().Property(link => link.Id).ValueGeneratedOnAdd();

      builder.Entity<Credentials>().HasKey(credentials => new { credentials.ServiceLinkId, credentials.UserId });

      builder.Entity<ServiceLink>()
        .HasOne(link => link.Credentials)
        .WithOne(creds => creds.ServiceLink)
        .HasForeignKey<Credentials>(credentials => new { credentials.ServiceLinkId, credentials.UserId });

      builder.Entity<ServiceLink>()
        .HasMany(link => link.ManagedAccounts)
        .WithOne(account => account.ServiceLink)
        .HasForeignKey(account => new { account.ServiceLinkId, account.ServiceLinkUserId });

      builder.Entity<User>()
        .HasMany(user => user.LocalAccounts)
        .WithOne(account => account.User)
        .HasForeignKey(account => account.UserId);

      builder.Entity<User>()
        .HasOne(user => user.TargetPortfolio)
        .WithOne(portfolio => portfolio.User)
        .HasForeignKey<TargetPortfolio>(portfolio => portfolio.UserId);

      builder.Entity<TargetPortfolio>()
        .HasMany(portfolio => portfolio.Allocations)
        .WithOne(allocation => allocation.Portfolio)
        .HasForeignKey(portfolio => portfolio.PortfolioUserId);
    }
  }
}
