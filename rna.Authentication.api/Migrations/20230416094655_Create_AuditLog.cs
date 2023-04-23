using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rna.Authorization.Application.Migrations
{
    public partial class Create_AuditLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TellerRegister_Teller_TellerId",
                table: "TellerRegister");

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    DepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TellerRegister_Teller_TellerId",
                table: "TellerRegister",
                column: "TellerId",
                principalTable: "Teller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TellerRegister_Teller_TellerId",
                table: "TellerRegister");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.AddForeignKey(
                name: "FK_TellerRegister_Teller_TellerId",
                table: "TellerRegister",
                column: "TellerId",
                principalTable: "Teller",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
