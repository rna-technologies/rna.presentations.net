using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Application.Migrations
{
    public partial class _20191212193851_StructureModification_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Document_Name",
                table: "Document");

            migrationBuilder.AlterColumn<int>(
                name: "AppId",
                table: "Document",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_Name_AppId",
                table: "Document",
                columns: new[] { "Name", "AppId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Document_Name_AppId",
                table: "Document");

            migrationBuilder.AlterColumn<int>(
                name: "AppId",
                table: "Document",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Document_Name",
                table: "Document",
                column: "Name",
                unique: true);
        }
    }
}
