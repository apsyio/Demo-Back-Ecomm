using Microsoft.EntityFrameworkCore.Migrations;

namespace Aps.Apps.CueTheCurves.Api.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colors",
                table: "Styles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Colors",
                table: "Styles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
