using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rna.Authorization.Application.Migrations
{
    /// <inheritdoc />
    public partial class GroupAppDepartmentRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ScopeClaim_AppId",
                table: "ScopeClaim",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_ScopeClaim_DepartmentId",
                table: "ScopeClaim",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScopeClaim_GroupId",
                table: "ScopeClaim",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationInfo_AppId",
                table: "RegistrationInfo",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationInfo_GroupId",
                table: "RegistrationInfo",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationInfo_App_AppId",
                table: "RegistrationInfo",
                column: "AppId",
                principalTable: "App",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationInfo_Group_GroupId",
                table: "RegistrationInfo",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScopeClaim_App_AppId",
                table: "ScopeClaim",
                column: "AppId",
                principalTable: "App",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScopeClaim_Department_DepartmentId",
                table: "ScopeClaim",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScopeClaim_Group_GroupId",
                table: "ScopeClaim",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationInfo_App_AppId",
                table: "RegistrationInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationInfo_Group_GroupId",
                table: "RegistrationInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ScopeClaim_App_AppId",
                table: "ScopeClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_ScopeClaim_Department_DepartmentId",
                table: "ScopeClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_ScopeClaim_Group_GroupId",
                table: "ScopeClaim");

            migrationBuilder.DropIndex(
                name: "IX_ScopeClaim_AppId",
                table: "ScopeClaim");

            migrationBuilder.DropIndex(
                name: "IX_ScopeClaim_DepartmentId",
                table: "ScopeClaim");

            migrationBuilder.DropIndex(
                name: "IX_ScopeClaim_GroupId",
                table: "ScopeClaim");

            migrationBuilder.DropIndex(
                name: "IX_RegistrationInfo_AppId",
                table: "RegistrationInfo");

            migrationBuilder.DropIndex(
                name: "IX_RegistrationInfo_GroupId",
                table: "RegistrationInfo");
        }
    }
}
