using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OPC.Data.Migrations
{
    public partial class _0922 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NodeClass",
                table: "NodeInfo",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NodeClass",
                table: "NodeInfo");
        }
    }
}
