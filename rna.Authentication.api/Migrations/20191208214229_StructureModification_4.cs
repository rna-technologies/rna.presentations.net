﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Application.Migrations
{
    public partial class StructureModification_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentCategoryType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
