using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rna.Authorization.Application.Migrations
{
    /// <inheritdoc />
    public partial class GroupProfile_Phones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber2",
                table: "GroupProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber3",
                table: "GroupProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber4",
                table: "GroupProfile",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "GroupProfile",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber2",
                table: "GroupProfile");

            migrationBuilder.DropColumn(
                name: "PhoneNumber3",
                table: "GroupProfile");

            migrationBuilder.DropColumn(
                name: "PhoneNumber4",
                table: "GroupProfile");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "GroupProfile");
        }
    }
}
