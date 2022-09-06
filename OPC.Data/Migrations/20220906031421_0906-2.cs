using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OPC.Data.Migrations
{
    public partial class _09062 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DataModel",
                table: "NodeInfo",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "NodeInfo",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "NodeInfo");

            migrationBuilder.AlterColumn<string>(
                name: "DataModel",
                table: "NodeInfo",
                type: "TEXT",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
