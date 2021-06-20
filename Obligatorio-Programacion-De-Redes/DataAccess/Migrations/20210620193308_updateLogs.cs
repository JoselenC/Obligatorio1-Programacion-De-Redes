using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObjectName",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObjectType",
                table: "Logs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjectName",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ObjectType",
                table: "Logs");
        }
    }
}
