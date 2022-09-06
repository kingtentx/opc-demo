using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OPC.Data.Migrations
{
    public partial class _0905 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NodeInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NodeId = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    NodeName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DataType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DataModel = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NodeInfo");
        }
    }
}
