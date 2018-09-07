﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ParityService.Data;

namespace ParityService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180907011013_AccountCash")]
    partial class AccountCash
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ParityService.Models.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName");

                    b.Property<int>("AccountType");

                    b.Property<decimal>("Cash");

                    b.Property<decimal?>("ContributionRoom");

                    b.Property<int>("ServiceLinkId");

                    b.Property<string>("ServiceLinkUserId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("ServiceLinkId", "ServiceLinkUserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ParityService.Models.Entities.Credentials", b =>
                {
                    b.Property<int>("ServiceLinkId");

                    b.Property<string>("UserId");

                    b.Property<string>("AccessToken");

                    b.Property<DateTime>("AccessTokenExpiresAt");

                    b.Property<string>("AccessTokenType");

                    b.Property<string>("ApiServer");

                    b.Property<string>("RefreshToken");

                    b.HasKey("ServiceLinkId", "UserId");

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("ParityService.Models.Entities.Earnings", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<decimal>("AnnualIncome");

                    b.Property<int>("Region");

                    b.HasKey("UserId");

                    b.ToTable("Earnings");
                });

            modelBuilder.Entity("ParityService.Models.Entities.ServiceLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("IsPractice");

                    b.Property<string>("ServiceId")
                        .IsRequired();

                    b.Property<int>("ServiceType");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ServiceLinks");
                });

            modelBuilder.Entity("ParityService.Models.Entities.TargetAllocation", b =>
                {
                    b.Property<string>("PortfolioUserId");

                    b.Property<string>("Symbol");

                    b.Property<decimal>("Proportion");

                    b.HasKey("PortfolioUserId", "Symbol");

                    b.ToTable("TargetAllocations");
                });

            modelBuilder.Entity("ParityService.Models.Entities.TargetPortfolio", b =>
                {
                    b.Property<string>("UserId");

                    b.HasKey("UserId");

                    b.ToTable("TargetPortfolios");
                });

            modelBuilder.Entity("ParityService.Models.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ParityService.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ParityService.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ParityService.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ParityService.Models.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ParityService.Models.Entities.Account", b =>
                {
                    b.HasOne("ParityService.Models.Entities.User", "User")
                        .WithMany("LocalAccounts")
                        .HasForeignKey("UserId");

                    b.HasOne("ParityService.Models.Entities.ServiceLink", "ServiceLink")
                        .WithMany("ManagedAccounts")
                        .HasForeignKey("ServiceLinkId", "ServiceLinkUserId");
                });

            modelBuilder.Entity("ParityService.Models.Entities.Credentials", b =>
                {
                    b.HasOne("ParityService.Models.Entities.ServiceLink", "ServiceLink")
                        .WithOne("Credentials")
                        .HasForeignKey("ParityService.Models.Entities.Credentials", "ServiceLinkId", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ParityService.Models.Entities.Earnings", b =>
                {
                    b.HasOne("ParityService.Models.Entities.User")
                        .WithOne("Earnings")
                        .HasForeignKey("ParityService.Models.Entities.Earnings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ParityService.Models.Entities.ServiceLink", b =>
                {
                    b.HasOne("ParityService.Models.Entities.User")
                        .WithMany("ServiceLinks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ParityService.Models.Entities.TargetAllocation", b =>
                {
                    b.HasOne("ParityService.Models.Entities.TargetPortfolio", "Portfolio")
                        .WithMany("Allocations")
                        .HasForeignKey("PortfolioUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ParityService.Models.Entities.TargetPortfolio", b =>
                {
                    b.HasOne("ParityService.Models.Entities.User", "User")
                        .WithOne("TargetPortfolio")
                        .HasForeignKey("ParityService.Models.Entities.TargetPortfolio", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
