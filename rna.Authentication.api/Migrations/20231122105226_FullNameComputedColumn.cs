using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rna.Authorization.Application.Migrations
{
    /// <inheritdoc />
    public partial class FullNameComputedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "User",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                computedColumnSql: "[LastName] + ' ' + [FirstName] + ' ' + [MiddleName] PERSISTED",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(MAX)",
                oldMaxLength: 450,
                oldDefaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "User",
                type: "NVARCHAR(MAX)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldComputedColumnSql: "[LastName] + ' ' + [FirstName] + ' ' + [MiddleName] PERSISTED");
        }
    }
}
