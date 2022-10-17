using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rna.Authorization.Application.Migrations;

public partial class InitialMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "App",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_App", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AppDocumentCategory",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                AppId = table.Column<int>(nullable: false),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppDocumentCategory", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AppDocumentCategoryType",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                AppId = table.Column<int>(nullable: false),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppDocumentCategoryType", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                Id = table.Column<string>(nullable: false),
                Name = table.Column<string>(maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
            {
                Id = table.Column<string>(nullable: false),
                UserName = table.Column<string>(maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                Email = table.Column<string>(maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(nullable: false),
                PasswordHash = table.Column<string>(nullable: true),
                SecurityStamp = table.Column<string>(nullable: true),
                ConcurrencyStamp = table.Column<string>(nullable: true),
                PhoneNumber = table.Column<string>(nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                TwoFactorEnabled = table.Column<bool>(nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                LockoutEnabled = table.Column<bool>(nullable: false),
                AccessFailedCount = table.Column<int>(nullable: false),
                AccountEnabled = table.Column<bool>(nullable: false),
                IsTellerable = table.Column<bool>(nullable: false, defaultValue: true),
                FullName = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true, computedColumnSql: "[LastName] + ' ' + [FirstName]+ ' ' + [MiddleName] PERSISTED"),
                NextLoginChangePassword = table.Column<bool>(nullable: false),
                FirstName = table.Column<string>(nullable: true, defaultValue: ""),
                LastName = table.Column<string>(nullable: true, defaultValue: ""),
                MiddleName = table.Column<string>(nullable: true, defaultValue: ""),
                RegisteredGroupId = table.Column<int>(nullable: true),
                ExternalAuthId = table.Column<long>(nullable: false),
                PictureUrl = table.Column<string>(nullable: true),
                ZipCode = table.Column<string>(nullable: true),
                ExternalAuthName = table.Column<string>(nullable: true),
                Gender = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserModel",
            columns: table => new
            {
                Id = table.Column<string>(nullable: false),
                PhoneNumber = table.Column<string>(nullable: true),
                Email = table.Column<string>(nullable: true),
                FullName = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserModel", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Document",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                IsGrantDocument = table.Column<bool>(nullable: false),
                AppId = table.Column<int>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Document", x => x.Id);
                table.ForeignKey(
                    name: "FK_Document_App_AppId",
                    column: x => x.AppId,
                    principalTable: "App",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Role",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                AppId = table.Column<int>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Role", x => x.Id);
                table.ForeignKey(
                    name: "FK_Role_App_AppId",
                    column: x => x.AppId,
                    principalTable: "App",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                RoleId = table.Column<string>(nullable: false),
                ClaimType = table.Column<string>(nullable: true),
                ClaimValue = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AppClaim",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                IsAtive = table.Column<bool>(nullable: false),
                SuiteUserId = table.Column<string>(nullable: true),
                AppId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_AppClaim_App_AppId",
                    column: x => x.AppId,
                    principalTable: "App",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AppClaim_AspNetUsers_SuiteUserId",
                    column: x => x.SuiteUserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                UserId = table.Column<string>(nullable: false),
                ClaimType = table.Column<string>(nullable: true),
                ClaimValue = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            columns: table => new
            {
                LoginProvider = table.Column<string>(nullable: false),
                ProviderKey = table.Column<string>(nullable: false),
                ProviderDisplayName = table.Column<string>(nullable: true),
                UserId = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            columns: table => new
            {
                UserId = table.Column<string>(nullable: false),
                RoleId = table.Column<string>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            columns: table => new
            {
                UserId = table.Column<string>(nullable: false),
                LoginProvider = table.Column<string>(nullable: false),
                Name = table.Column<string>(nullable: false),
                Value = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RegisterationInfo",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                GroupId = table.Column<int>(nullable: false),
                RegistrarId = table.Column<string>(nullable: true),
                GroupAssigned = table.Column<bool>(nullable: false, defaultValue: false),
                Date = table.Column<DateTime>(nullable: false),
                UserId = table.Column<string>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RegisterationInfo", x => x.Id);
                table.ForeignKey(
                    name: "FK_RegisterationInfo_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "DocumentCategory",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                DocumentId = table.Column<int>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DocumentCategory", x => x.Id);
                table.ForeignKey(
                    name: "FK_DocumentCategory_Document_DocumentId",
                    column: x => x.DocumentId,
                    principalTable: "Document",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "DocumentCategoryType",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                DocumentId = table.Column<int>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DocumentCategoryType", x => x.Id);
                table.ForeignKey(
                    name: "FK_DocumentCategoryType_Document_DocumentId",
                    column: x => x.DocumentId,
                    principalTable: "Document",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "GroupClaim",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                GroupId = table.Column<int>(nullable: false),
                IsActive = table.Column<bool>(nullable: false),
                AppClaimId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GroupClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_GroupClaim_AppClaim_AppClaimId",
                    column: x => x.AppClaimId,
                    principalTable: "AppClaim",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RoleClaim",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                RoleId = table.Column<int>(nullable: true),
                IsActive = table.Column<bool>(nullable: false),
                ApplyCustomClaims = table.Column<bool>(nullable: false),
                GroupClaimId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RoleClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_RoleClaim_GroupClaim_GroupClaimId",
                    column: x => x.GroupClaimId,
                    principalTable: "GroupClaim",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_RoleClaim_Role_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "CategoryTypeClaim",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                DocumentCategoryTypeId = table.Column<int>(nullable: false),
                Create = table.Column<bool>(nullable: false),
                Read = table.Column<bool>(nullable: false),
                Update = table.Column<bool>(nullable: false),
                Delete = table.Column<bool>(nullable: false),
                CategoryClaimId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CategoryTypeClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_CategoryTypeClaim_DocumentCategoryType_DocumentCategoryTypeId",
                    column: x => x.DocumentCategoryTypeId,
                    principalTable: "DocumentCategoryType",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "DocumentClaim",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                IsActive = table.Column<bool>(nullable: false),
                DocumentId = table.Column<int>(nullable: true),
                RoleId = table.Column<int>(nullable: true),
                RoleClaimId = table.Column<int>(nullable: true),
                CustomGrantClaimId = table.Column<int>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DocumentClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_DocumentClaim_Document_DocumentId",
                    column: x => x.DocumentId,
                    principalTable: "Document",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_DocumentClaim_RoleClaim_RoleClaimId",
                    column: x => x.RoleClaimId,
                    principalTable: "RoleClaim",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_DocumentClaim_Role_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "CategoryClaim",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                DocumentCategoryId = table.Column<int>(nullable: false),
                DocumentClaimId = table.Column<int>(nullable: false),
                IsActive = table.Column<bool>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CategoryClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_CategoryClaim_DocumentCategory_DocumentCategoryId",
                    column: x => x.DocumentCategoryId,
                    principalTable: "DocumentCategory",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CategoryClaim_DocumentClaim_DocumentClaimId",
                    column: x => x.DocumentClaimId,
                    principalTable: "DocumentClaim",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "GrantTypeClaim",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                IsRoleGrant = table.Column<bool>(nullable: false),
                DocumentClaimId = table.Column<int>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GrantTypeClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_GrantTypeClaim_DocumentClaim_DocumentClaimId",
                    column: x => x.DocumentClaimId,
                    principalTable: "DocumentClaim",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "CustomGrantClaim",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                GrantTypeClaimId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CustomGrantClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_CustomGrantClaim_GrantTypeClaim_GrantTypeClaimId",
                    column: x => x.GrantTypeClaimId,
                    principalTable: "GrantTypeClaim",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RoleGrantClaim",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                IsActive = table.Column<bool>(nullable: false),
                GrantTypeClaimId = table.Column<int>(nullable: true),
                RoleId = table.Column<int>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RoleGrantClaim", x => x.Id);
                table.ForeignKey(
                    name: "FK_RoleGrantClaim_GrantTypeClaim_GrantTypeClaimId",
                    column: x => x.GrantTypeClaimId,
                    principalTable: "GrantTypeClaim",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_RoleGrantClaim_Role_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AppClaim_AppId",
            table: "AppClaim",
            column: "AppId");

        migrationBuilder.CreateIndex(
            name: "IX_AppClaim_SuiteUserId",
            table: "AppClaim",
            column: "SuiteUserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetRoleClaims_RoleId",
            table: "AspNetRoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            table: "AspNetRoles",
            column: "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserClaims_UserId",
            table: "AspNetUserClaims",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserLogins_UserId",
            table: "AspNetUserLogins",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserRoles_RoleId",
            table: "AspNetUserRoles",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "AspNetUsers",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "AspNetUsers",
            column: "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_CategoryClaim_DocumentClaimId",
            table: "CategoryClaim",
            column: "DocumentClaimId");

        migrationBuilder.CreateIndex(
            name: "IX_CategoryClaim_DocumentCategoryId_DocumentClaimId",
            table: "CategoryClaim",
            columns: new[] { "DocumentCategoryId", "DocumentClaimId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_CategoryTypeClaim_DocumentCategoryTypeId",
            table: "CategoryTypeClaim",
            column: "DocumentCategoryTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_CategoryTypeClaim_CategoryClaimId_DocumentCategoryTypeId",
            table: "CategoryTypeClaim",
            columns: new[] { "CategoryClaimId", "DocumentCategoryTypeId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_CustomGrantClaim_GrantTypeClaimId",
            table: "CustomGrantClaim",
            column: "GrantTypeClaimId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Document_AppId",
            table: "Document",
            column: "AppId");

        migrationBuilder.CreateIndex(
            name: "IX_DocumentCategory_DocumentId",
            table: "DocumentCategory",
            column: "DocumentId");

        migrationBuilder.CreateIndex(
            name: "IX_DocumentCategoryType_DocumentId",
            table: "DocumentCategoryType",
            column: "DocumentId");

        migrationBuilder.CreateIndex(
            name: "IX_DocumentClaim_CustomGrantClaimId",
            table: "DocumentClaim",
            column: "CustomGrantClaimId");

        migrationBuilder.CreateIndex(
            name: "IX_DocumentClaim_DocumentId",
            table: "DocumentClaim",
            column: "DocumentId");

        migrationBuilder.CreateIndex(
            name: "IX_DocumentClaim_RoleClaimId",
            table: "DocumentClaim",
            column: "RoleClaimId");

        migrationBuilder.CreateIndex(
            name: "IX_DocumentClaim_RoleId",
            table: "DocumentClaim",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_GrantTypeClaim_DocumentClaimId",
            table: "GrantTypeClaim",
            column: "DocumentClaimId");

        migrationBuilder.CreateIndex(
            name: "IX_GroupClaim_AppClaimId",
            table: "GroupClaim",
            column: "AppClaimId");

        migrationBuilder.CreateIndex(
            name: "IX_RegisterationInfo_UserId",
            table: "RegisterationInfo",
            column: "UserId",
            unique: true,
            filter: "[UserId] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_Role_AppId",
            table: "Role",
            column: "AppId");

        migrationBuilder.CreateIndex(
            name: "IX_RoleClaim_GroupClaimId",
            table: "RoleClaim",
            column: "GroupClaimId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_RoleClaim_RoleId",
            table: "RoleClaim",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_RoleGrantClaim_GrantTypeClaimId",
            table: "RoleGrantClaim",
            column: "GrantTypeClaimId");

        migrationBuilder.CreateIndex(
            name: "IX_RoleGrantClaim_RoleId",
            table: "RoleGrantClaim",
            column: "RoleId");

        migrationBuilder.AddForeignKey(
            name: "FK_CategoryTypeClaim_CategoryClaim_CategoryClaimId",
            table: "CategoryTypeClaim",
            column: "CategoryClaimId",
            principalTable: "CategoryClaim",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_DocumentClaim_CustomGrantClaim_CustomGrantClaimId",
            table: "DocumentClaim",
            column: "CustomGrantClaimId",
            principalTable: "CustomGrantClaim",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AppClaim_App_AppId",
            table: "AppClaim");

        migrationBuilder.DropForeignKey(
            name: "FK_Document_App_AppId",
            table: "Document");

        migrationBuilder.DropForeignKey(
            name: "FK_Role_App_AppId",
            table: "Role");

        migrationBuilder.DropForeignKey(
            name: "FK_AppClaim_AspNetUsers_SuiteUserId",
            table: "AppClaim");

        migrationBuilder.DropForeignKey(
            name: "FK_GrantTypeClaim_DocumentClaim_DocumentClaimId",
            table: "GrantTypeClaim");

        migrationBuilder.DropTable(
            name: "AppDocumentCategory");

        migrationBuilder.DropTable(
            name: "AppDocumentCategoryType");

        migrationBuilder.DropTable(
            name: "AspNetRoleClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserClaims");

        migrationBuilder.DropTable(
            name: "AspNetUserLogins");

        migrationBuilder.DropTable(
            name: "AspNetUserRoles");

        migrationBuilder.DropTable(
            name: "AspNetUserTokens");

        migrationBuilder.DropTable(
            name: "CategoryTypeClaim");

        migrationBuilder.DropTable(
            name: "RegisterationInfo");

        migrationBuilder.DropTable(
            name: "RoleGrantClaim");

        migrationBuilder.DropTable(
            name: "UserModel");

        migrationBuilder.DropTable(
            name: "AspNetRoles");

        migrationBuilder.DropTable(
            name: "CategoryClaim");

        migrationBuilder.DropTable(
            name: "DocumentCategoryType");

        migrationBuilder.DropTable(
            name: "DocumentCategory");

        migrationBuilder.DropTable(
            name: "App");

        migrationBuilder.DropTable(
            name: "AspNetUsers");

        migrationBuilder.DropTable(
            name: "DocumentClaim");

        migrationBuilder.DropTable(
            name: "CustomGrantClaim");

        migrationBuilder.DropTable(
            name: "Document");

        migrationBuilder.DropTable(
            name: "RoleClaim");

        migrationBuilder.DropTable(
            name: "GrantTypeClaim");

        migrationBuilder.DropTable(
            name: "GroupClaim");

        migrationBuilder.DropTable(
            name: "Role");

        migrationBuilder.DropTable(
            name: "AppClaim");
    }
}
