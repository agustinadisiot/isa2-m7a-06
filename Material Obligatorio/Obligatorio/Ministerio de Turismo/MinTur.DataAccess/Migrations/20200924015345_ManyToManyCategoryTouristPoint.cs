using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ManyToManyCategoryTouristPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_TouristPoints_TouristPointId",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_TouristPointId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "TouristPointId",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TouristPointCategory",
                columns: table => new
                {
                    TourisPointId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TouristPointCategory", x => new { x.CategoryId, x.TourisPointId });
                    table.ForeignKey(
                        name: "FK_TouristPointCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TouristPointCategory_TouristPoints_TourisPointId",
                        column: x => x.TourisPointId,
                        principalTable: "TouristPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TouristPointCategory_TourisPointId",
                table: "TouristPointCategory",
                column: "TourisPointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TouristPointCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.AddColumn<int>(
                name: "TouristPointId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Category_TouristPointId",
                table: "Category",
                column: "TouristPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_TouristPoints_TouristPointId",
                table: "Category",
                column: "TouristPointId",
                principalTable: "TouristPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
