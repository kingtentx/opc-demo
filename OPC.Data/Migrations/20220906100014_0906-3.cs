using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OPC.Data.Migrations
{
    public partial class _09063 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "User",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "User");
        }
    }
}
