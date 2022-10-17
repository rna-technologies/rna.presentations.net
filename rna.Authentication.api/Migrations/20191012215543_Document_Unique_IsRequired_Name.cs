using Microsoft.EntityFrameworkCore.Migrations;

namespace rna.Authorization.Application.Migrations
{
    public partial class Document_Unique_IsRequired_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Document",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_Name",
                table: "Document",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Document_Name",
                table: "Document");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Document",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
