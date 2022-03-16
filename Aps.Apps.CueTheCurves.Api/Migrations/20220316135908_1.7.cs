using Microsoft.EntityFrameworkCore.Migrations;

namespace Aps.Apps.CueTheCurves.Api.Migrations
{
    public partial class _17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeOffered",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "SizeOffered",
                table: "Brands",
                newName: "BrandUrl");

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrandSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandSizes_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SizeId",
                table: "Posts",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSizes_BrandId",
                table: "BrandSizes",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSizes_SizeId",
                table: "BrandSizes",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Sizes_SizeId",
                table: "Posts",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Sizes_SizeId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "BrandSizes");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SizeId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "BrandUrl",
                table: "Brands",
                newName: "SizeOffered");

            migrationBuilder.AddColumn<string>(
                name: "SizeOffered",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
