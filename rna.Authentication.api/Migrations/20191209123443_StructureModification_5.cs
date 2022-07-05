using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Application.Migrations
{
    public partial class StructureModification_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_App_AppId",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_App_AppId",
                table: "Role");

            migrationBuilder.DropForeignKey(
                name: "FK_ScopeClaim_App_AppId",
                table: "ScopeClaim");

            migrationBuilder.DropTable(
                name: "App");

            migrationBuilder.DropIndex(
                name: "IX_ScopeClaim_AppId",
                table: "ScopeClaim");

            migrationBuilder.DropIndex(
                name: "IX_Role_AppId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Document_AppId",
                table: "Document");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScopeClaim_AppId",
                table: "ScopeClaim",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_AppId",
                table: "Role",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_AppId",
                table: "Document",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_App_AppId",
                table: "Document",
                column: "AppId",
                principalTable: "App",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_App_AppId",
                table: "Role",
                column: "AppId",
                principalTable: "App",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScopeClaim_App_AppId",
                table: "ScopeClaim",
                column: "AppId",
                principalTable: "App",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
