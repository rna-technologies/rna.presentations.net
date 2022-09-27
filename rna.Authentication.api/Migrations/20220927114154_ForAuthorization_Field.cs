using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Application.Migrations
{
    public partial class ForAuthorization_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForAuthorization",
                table: "App",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForAuthorization",
                table: "App");
        }
    }
}
