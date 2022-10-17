using Microsoft.EntityFrameworkCore.Migrations;

namespace rna.Authorization.Application.Migrations
{
    public partial class StructureModification_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Validation",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Validation",
                table: "AspNetUsers");
        }
    }
}
