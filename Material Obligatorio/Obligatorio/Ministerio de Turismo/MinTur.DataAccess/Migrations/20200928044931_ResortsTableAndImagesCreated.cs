using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MinTur.DataAccess.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ResortsTableAndImagesCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "TouristPoints");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "TouristPoints",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Resorts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TouristPointId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Stars = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PricePerNight = table.Column<int>(nullable: false),
                    Available = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resorts_TouristPoints_TouristPointId",
                        column: x => x.TouristPointId,
                        principalTable: "TouristPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(nullable: true),
                    ResortId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Resorts_ResortId",
                        column: x => x.ResortId,
                        principalTable: "Resorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TouristPoints_ImageId",
                table: "TouristPoints",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ResortId",
                table: "Images",
                column: "ResortId");

            migrationBuilder.CreateIndex(
                name: "IX_Resorts_TouristPointId",
                table: "Resorts",
                column: "TouristPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristPoints_Images_ImageId",
                table: "TouristPoints",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristPoints_Images_ImageId",
                table: "TouristPoints");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Resorts");

            migrationBuilder.DropIndex(
                name: "IX_TouristPoints_ImageId",
                table: "TouristPoints");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "TouristPoints");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "TouristPoints",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
