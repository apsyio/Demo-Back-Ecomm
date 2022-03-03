using Microsoft.EntityFrameworkCore.Migrations;

namespace Aps.Apps.CueTheCurves.Api.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropColumn(
                name: "SizeTag",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SizeTag",
                table: "Brands");

            migrationBuilder.AddColumn<string>(
                name: "SizeOffered",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeOffered",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeOffered",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SizeOffered",
                table: "Brands");

            migrationBuilder.AddColumn<double>(
                name: "SizeTag",
                table: "Posts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "SizeTag",
                table: "Brands",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });
        }
    }
}
