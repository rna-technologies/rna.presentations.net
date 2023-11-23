using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rna.Authorization.Application.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFullNameComputedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "User",
                type: "NVARCHAR(MAX)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldComputedColumnSql: "[LastName] + ' ' + [FirstName]+ ' ' + [MiddleName] PERSISTED");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "User",
                type: "NVARCHAR(MAX)",
                nullable: false,
                computedColumnSql: "[LastName] + ' ' + [FirstName]+ ' ' + [MiddleName] PERSISTED",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldDefaultValue: "");
        }
    }
}
