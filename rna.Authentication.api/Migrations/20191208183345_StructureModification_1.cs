using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Application.Migrations
{
    public partial class StructureModification_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CategoryTypeClaim_DocumentCategoryType_DocumentCategoryTypeId",
            //    table: "CategoryTypeClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaim_GroupClaim_GroupClaimId",
                table: "RoleClaim");

            migrationBuilder.DropTable(
                name: "AppDocumentCategoryType");

            //migrationBuilder.DropTable(
            //    name: "DocumentCategoryType");

            migrationBuilder.DropTable(
                name: "GroupClaim");

            migrationBuilder.DropTable(
                name: "AppClaim");

            migrationBuilder.RenameColumn(
                name: "GroupClaimId",
                table: "RoleClaim",
                newName: "ScopeClaimId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaim_GroupClaimId",
                table: "RoleClaim",
                newName: "IX_RoleClaim_ScopeClaimId");

            migrationBuilder.CreateTable(
                name: "ScopeClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    AppId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    AccountTypeId = table.Column<int>(nullable: false),
                    IsAtive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScopeClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScopeClaim_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScopeClaim_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_CategoryTypeClaim_CategoryClaimId",
            //    table: "CategoryTypeClaim",
            //    column: "CategoryClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_ScopeClaim_AppId",
                table: "ScopeClaim",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_ScopeClaim_UserId_AppId_GroupId_AccountTypeId",
                table: "ScopeClaim",
                columns: new[] { "UserId", "AppId", "GroupId", "AccountTypeId" },
                unique: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_RoleClaim_ScopeClaim_ScopeClaimId",
            //    table: "RoleClaim",
            //    column: "ScopeClaimId",
            //    principalTable: "ScopeClaim",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaim_ScopeClaim_ScopeClaimId",
                table: "RoleClaim");

            migrationBuilder.DropTable(
                name: "ScopeClaim");

            migrationBuilder.DropIndex(
                name: "IX_CategoryTypeClaim_CategoryClaimId",
                table: "CategoryTypeClaim");

            migrationBuilder.RenameColumn(
                name: "ScopeClaimId",
                table: "RoleClaim",
                newName: "GroupClaimId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaim_ScopeClaimId",
                table: "RoleClaim",
                newName: "IX_RoleClaim_GroupClaimId");

            migrationBuilder.AddColumn<int>(
                name: "DocumentCategoryTypeId",
                table: "CategoryTypeClaim",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppId = table.Column<int>(nullable: false),
                    IsAtive = table.Column<bool>(nullable: false),
                    SuiteUserId = table.Column<string>(nullable: true)
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
                name: "AppDocumentCategoryType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDocumentCategoryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCategoryType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    DocumentId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
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
                    AppClaimId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
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
                name: "IX_AppClaim_AppId",
                table: "AppClaim",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_AppClaim_SuiteUserId",
                table: "AppClaim",
                column: "SuiteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCategoryType_DocumentId_Name",
                table: "DocumentCategoryType",
                columns: new[] { "DocumentId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupClaim_AppClaimId",
                table: "GroupClaim",
                column: "AppClaimId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryTypeClaim_DocumentCategoryType_DocumentCategoryTypeId",
                table: "CategoryTypeClaim",
                column: "DocumentCategoryTypeId",
                principalTable: "DocumentCategoryType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaim_GroupClaim_GroupClaimId",
                table: "RoleClaim",
                column: "GroupClaimId",
                principalTable: "GroupClaim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
