using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class FixIncorrectColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristPointCategory_TouristPoints_TourisPointId",
                table: "TouristPointCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TouristPointCategory",
                table: "TouristPointCategory");

            migrationBuilder.DropIndex(
                name: "IX_TouristPointCategory_TourisPointId",
                table: "TouristPointCategory");

            migrationBuilder.DropColumn(
                name: "TourisPointId",
                table: "TouristPointCategory");

            migrationBuilder.AddColumn<int>(
                name: "TouristPointId",
                table: "TouristPointCategory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TouristPointCategory",
                table: "TouristPointCategory",
                columns: new[] { "CategoryId", "TouristPointId" });

            migrationBuilder.CreateIndex(
                name: "IX_TouristPointCategory_TouristPointId",
                table: "TouristPointCategory",
                column: "TouristPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristPointCategory_TouristPoints_TouristPointId",
                table: "TouristPointCategory",
                column: "TouristPointId",
                principalTable: "TouristPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristPointCategory_TouristPoints_TouristPointId",
                table: "TouristPointCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TouristPointCategory",
                table: "TouristPointCategory");

            migrationBuilder.DropIndex(
                name: "IX_TouristPointCategory_TouristPointId",
                table: "TouristPointCategory");

            migrationBuilder.DropColumn(
                name: "TouristPointId",
                table: "TouristPointCategory");

            migrationBuilder.AddColumn<int>(
                name: "TourisPointId",
                table: "TouristPointCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TouristPointCategory",
                table: "TouristPointCategory",
                columns: new[] { "CategoryId", "TourisPointId" });

            migrationBuilder.CreateIndex(
                name: "IX_TouristPointCategory_TourisPointId",
                table: "TouristPointCategory",
                column: "TourisPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristPointCategory_TouristPoints_TourisPointId",
                table: "TouristPointCategory",
                column: "TourisPointId",
                principalTable: "TouristPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
