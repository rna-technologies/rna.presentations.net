﻿// <auto-generated />
using System;
using Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Identity.Application.Migrations
{
    [DbContext(typeof(SuiteIdentityContext))]
    [Migration("20191212193851_StructureModification_6")]
    partial class StructureModification_6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Identity.Domain.Entities.AppDocumentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("AppDocumentCategory");
                });

            modelBuilder.Entity("Identity.Domain.Entities.CategoryClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DocumentCategoryId");

                    b.Property<int>("DocumentClaimId");

                    b.Property<bool>("IsActive");

                    b.HasKey("Id");

                    b.HasIndex("DocumentClaimId");

                    b.HasIndex("DocumentCategoryId", "DocumentClaimId")
                        .IsUnique();

                    b.ToTable("CategoryClaim");
                });

            modelBuilder.Entity("Identity.Domain.Entities.CategoryTypeClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryClaimId");

                    b.Property<bool>("Create");

                    b.Property<bool>("Delete");

                    b.Property<bool>("Read");

                    b.Property<bool>("Update");

                    b.HasKey("Id");

                    b.HasIndex("CategoryClaimId");

                    b.ToTable("CategoryTypeClaim");
                });

            modelBuilder.Entity("Identity.Domain.Entities.CustomGrantClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GrantTypeClaimId");

                    b.HasKey("Id");

                    b.HasIndex("GrantTypeClaimId")
                        .IsUnique();

                    b.ToTable("CustomGrantClaim");
                });

            modelBuilder.Entity("Identity.Domain.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppId");

                    b.Property<string>("Description");

                    b.Property<bool>("IsGrantDocument");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Document");
                });

            modelBuilder.Entity("Identity.Domain.Entities.DocumentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("DocumentId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DocumentId", "Name")
                        .IsUnique();

                    b.ToTable("DocumentCategory");
                });

            modelBuilder.Entity("Identity.Domain.Entities.DocumentClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomGrantClaimId");

                    b.Property<int?>("DocumentId");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("RoleClaimId");

                    b.Property<int?>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("CustomGrantClaimId");

                    b.HasIndex("DocumentId");

                    b.HasIndex("RoleClaimId");

                    b.HasIndex("RoleId");

                    b.ToTable("DocumentClaim");
                });

            modelBuilder.Entity("Identity.Domain.Entities.GrantTypeClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DocumentClaimId");

                    b.Property<bool>("IsRoleGrant");

                    b.HasKey("Id");

                    b.HasIndex("DocumentClaimId");

                    b.ToTable("GrantTypeClaim");
                });

            modelBuilder.Entity("Identity.Domain.Entities.RegisterationInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppId");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("GroupAssigned")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("GroupId");

                    b.Property<string>("RegistrarId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("RegisterationInfo");
                });

            modelBuilder.Entity("Identity.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Identity.Domain.Entities.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ApplyCustomClaims");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("RoleId");

                    b.Property<int>("ScopeClaimId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("ScopeClaimId")
                        .IsUnique();

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Identity.Domain.Entities.RoleGrantClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GrantTypeClaimId");

                    b.Property<bool>("IsActive");

                    b.Property<int?>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("GrantTypeClaimId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleGrantClaim");
                });

            modelBuilder.Entity("Identity.Domain.Entities.ScopeClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountTypeId");

                    b.Property<int>("AppId");

                    b.Property<int>("GroupId");

                    b.Property<bool>("IsAtive");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId", "AppId", "GroupId", "AccountTypeId")
                        .IsUnique();

                    b.ToTable("ScopeClaim");
                });

            modelBuilder.Entity("Identity.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("AccountEnabled");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTimeOffset?>("DeletedDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<long>("ExternalAuthId");

                    b.Property<string>("ExternalAuthName");

                    b.Property<string>("FirstName")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<string>("FullName")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("NVARCHAR(MAX)")
                        .HasComputedColumnSql("[LastName] + ' ' + [FirstName]+ ' ' + [MiddleName] PERSISTED");

                    b.Property<string>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsTellerable")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("LastName")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("");

                    b.Property<bool>("NextLoginChangePassword");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("PictureUrl");

                    b.Property<int?>("RegisteredGroupId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("Validation");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Identity.Infrastructure.Models.UserModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("UserModel");
                });

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
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

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

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Identity.Domain.Entities.CategoryClaim", b =>
                {
                    b.HasOne("Identity.Domain.Entities.DocumentCategory", "DocumentCategory")
                        .WithMany()
                        .HasForeignKey("DocumentCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Identity.Domain.Entities.DocumentClaim", "DocumentClaim")
                        .WithMany("CategoryClaims")
                        .HasForeignKey("DocumentClaimId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Identity.Domain.Entities.CategoryTypeClaim", b =>
                {
                    b.HasOne("Identity.Domain.Entities.CategoryClaim", "CategoryClaim")
                        .WithMany("CategoryTypeClaims")
                        .HasForeignKey("CategoryClaimId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Identity.Domain.Entities.CustomGrantClaim", b =>
                {
                    b.HasOne("Identity.Domain.Entities.GrantTypeClaim", "GrantTypeClaim")
                        .WithOne("CustomGrantClaim")
                        .HasForeignKey("Identity.Domain.Entities.CustomGrantClaim", "GrantTypeClaimId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Identity.Domain.Entities.DocumentCategory", b =>
                {
                    b.HasOne("Identity.Domain.Entities.Document", "Document")
                        .WithMany("DocumentCategories")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Identity.Domain.Entities.DocumentClaim", b =>
                {
                    b.HasOne("Identity.Domain.Entities.CustomGrantClaim", "CustomGrantClaim")
                        .WithMany("DocumentClaims")
                        .HasForeignKey("CustomGrantClaimId");

                    b.HasOne("Identity.Domain.Entities.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId");

                    b.HasOne("Identity.Domain.Entities.RoleClaim", "RoleClaim")
                        .WithMany("DocumentClaims")
                        .HasForeignKey("RoleClaimId");

                    b.HasOne("Identity.Domain.Entities.Role", "Role")
                        .WithMany("DocumentClaims")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Identity.Domain.Entities.GrantTypeClaim", b =>
                {
                    b.HasOne("Identity.Domain.Entities.DocumentClaim", "DocumentClaim")
                        .WithMany("GrantTypeClaims")
                        .HasForeignKey("DocumentClaimId");
                });

            modelBuilder.Entity("Identity.Domain.Entities.RegisterationInfo", b =>
                {
                    b.HasOne("Identity.Domain.Entities.User", "User")
                        .WithOne("RegisterationInfo")
                        .HasForeignKey("Identity.Domain.Entities.RegisterationInfo", "UserId");
                });

            modelBuilder.Entity("Identity.Domain.Entities.RoleClaim", b =>
                {
                    b.HasOne("Identity.Domain.Entities.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId");

                    b.HasOne("Identity.Domain.Entities.ScopeClaim", "ScopeClaim")
                        .WithOne("RoleClaim")
                        .HasForeignKey("Identity.Domain.Entities.RoleClaim", "ScopeClaimId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Identity.Domain.Entities.RoleGrantClaim", b =>
                {
                    b.HasOne("Identity.Domain.Entities.GrantTypeClaim", "GrantTypeClaim")
                        .WithMany("RoleGrantClaims")
                        .HasForeignKey("GrantTypeClaimId");

                    b.HasOne("Identity.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Identity.Domain.Entities.ScopeClaim", b =>
                {
                    b.HasOne("Identity.Domain.Entities.User", "User")
                        .WithMany("ScopeClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                    b.HasOne("Identity.Domain.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Identity.Domain.Entities.User")
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

                    b.HasOne("Identity.Domain.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Identity.Domain.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
