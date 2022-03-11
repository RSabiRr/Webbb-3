using Microsoft.EntityFrameworkCore.Migrations;

namespace Universite_Web.Migrations
{
    public partial class asadqw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MapLink",
                table: "Settings",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MapLink",
                table: "Settings");
        }
    }
}
