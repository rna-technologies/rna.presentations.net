using Microsoft.EntityFrameworkCore.Migrations;

namespace rna.Authorization.Application.Migrations
{
    public partial class DocumentCategory_IsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentCategory_Document_DocumentId",
                table: "DocumentCategory");

            migrationBuilder.DropIndex(
                name: "IX_DocumentCategory_DocumentId",
                table: "DocumentCategory");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DocumentCategory",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentCategory",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCategory_DocumentId_Name",
                table: "DocumentCategory",
                columns: new[] { "DocumentId", "Name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentCategory_Document_DocumentId",
                table: "DocumentCategory",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentCategory_Document_DocumentId",
                table: "DocumentCategory");

            migrationBuilder.DropIndex(
                name: "IX_DocumentCategory_DocumentId_Name",
                table: "DocumentCategory");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DocumentCategory",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "DocumentId",
                table: "DocumentCategory",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCategory_DocumentId",
                table: "DocumentCategory",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentCategory_Document_DocumentId",
                table: "DocumentCategory",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
