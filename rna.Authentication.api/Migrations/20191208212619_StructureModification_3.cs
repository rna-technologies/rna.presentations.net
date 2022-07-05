using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Application.Migrations
{
    public partial class StructureModification_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScopeClaimId",
                table: "RoleClaim",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_ScopeClaimId",
                table: "RoleClaim",
                column: "ScopeClaimId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaim_ScopeClaim_ScopeClaimId",
                table: "RoleClaim",
                column: "ScopeClaimId",
                principalTable: "ScopeClaim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaim_ScopeClaim_ScopeClaimId",
                table: "RoleClaim");

            migrationBuilder.DropIndex(
                name: "IX_RoleClaim_ScopeClaimId",
                table: "RoleClaim");

            migrationBuilder.DropColumn(
                name: "ScopeClaimId",
                table: "RoleClaim");
        }
    }
}
