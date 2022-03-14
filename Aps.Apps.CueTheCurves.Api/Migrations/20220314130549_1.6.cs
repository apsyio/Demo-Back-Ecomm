using Microsoft.EntityFrameworkCore.Migrations;

namespace Aps.Apps.CueTheCurves.Api.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StyleBrands_Brands_BrandId",
                table: "StyleBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_StyleBrands_Styles_StyleId",
                table: "StyleBrands");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_StyleBrands_Brands_BrandId",
                table: "StyleBrands",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StyleBrands_Styles_StyleId",
                table: "StyleBrands",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StyleBrands_Brands_BrandId",
                table: "StyleBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_StyleBrands_Styles_StyleId",
                table: "StyleBrands");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_StyleBrands_Brands_BrandId",
                table: "StyleBrands",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StyleBrands_Styles_StyleId",
                table: "StyleBrands",
                column: "StyleId",
                principalTable: "Styles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
