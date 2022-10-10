using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.Application.Migrations
{
    public partial class teller_registration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    TellerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teller_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teller_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TellerRegister",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TellerId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistererId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CloserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TellerRegister", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TellerRegister_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TellerRegister_Teller_TellerId",
                        column: x => x.TellerId,
                        principalTable: "Teller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teller_AppId",
                table: "Teller",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Teller_UserId_AppId",
                table: "Teller",
                columns: new[] { "UserId", "AppId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TellerRegister_GroupId",
                table: "TellerRegister",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TellerRegister_TellerId",
                table: "TellerRegister",
                column: "TellerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TellerRegister");

            migrationBuilder.DropTable(
                name: "Teller");
        }
    }
}
