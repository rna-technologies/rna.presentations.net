using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Application.Migrations
{
    public partial class Rename_AccountTypeId_To_DepartmentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountTypeId",
                table: "ScopeClaim",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ScopeClaim_UserId_AppId_GroupId_AccountTypeId",
                table: "ScopeClaim",
                newName: "IX_ScopeClaim_UserId_AppId_GroupId_DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "ScopeClaim",
                newName: "AccountTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ScopeClaim_UserId_AppId_GroupId_DepartmentId",
                table: "ScopeClaim",
                newName: "IX_ScopeClaim_UserId_AppId_GroupId_AccountTypeId");
        }
    }
}
