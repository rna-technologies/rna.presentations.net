using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Application.Migrations
{
    public partial class DocumentCategoryType_IsRequired_IsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentCategoryType_DocumentId",
                table: "DocumentCategoryType");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DocumentCategoryType",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentCategoryType",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCategoryType_DocumentId_Name",
                table: "DocumentCategoryType",
                columns: new[] { "DocumentId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentCategoryType_DocumentId_Name",
                table: "DocumentCategoryType");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DocumentCategoryType",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentCategoryType",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCategoryType_DocumentId",
                table: "DocumentCategoryType",
                column: "DocumentId");
        }
    }
}
