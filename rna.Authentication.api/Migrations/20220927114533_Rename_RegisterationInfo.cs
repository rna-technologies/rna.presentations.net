using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rna.Authorization.Application.Migrations
{
    public partial class Rename_RegisterationInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegisterationInfo_AspNetUsers_UserId",
                table: "RegisterationInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisterationInfo",
                table: "RegisterationInfo");

            migrationBuilder.RenameTable(
                name: "RegisterationInfo",
                newName: "RegistrationInfo");

            migrationBuilder.RenameIndex(
                name: "IX_RegisterationInfo_UserId",
                table: "RegistrationInfo",
                newName: "IX_RegistrationInfo_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrationInfo",
                table: "RegistrationInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationInfo_AspNetUsers_UserId",
                table: "RegistrationInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationInfo_AspNetUsers_UserId",
                table: "RegistrationInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrationInfo",
                table: "RegistrationInfo");

            migrationBuilder.RenameTable(
                name: "RegistrationInfo",
                newName: "RegisterationInfo");

            migrationBuilder.RenameIndex(
                name: "IX_RegistrationInfo_UserId",
                table: "RegisterationInfo",
                newName: "IX_RegisterationInfo_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisterationInfo",
                table: "RegisterationInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegisterationInfo_AspNetUsers_UserId",
                table: "RegisterationInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
