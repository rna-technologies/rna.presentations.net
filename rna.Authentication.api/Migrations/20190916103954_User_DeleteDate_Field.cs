using Microsoft.EntityFrameworkCore.Migrations;

namespace rna.Authorization.Application.Migrations
{
    public partial class User_DeleteDate_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppId",
                table: "RegisterationInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedDate",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppId",
                table: "RegisterationInfo");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "AspNetUsers");
        }
    }
}
